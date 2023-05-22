using MediatR;
using Todo.Application.Commands.BoardCommands;
using Todo.Application.Exceptions;
using Todo.Application.Queries;
using Todo.Application.Results;
using Todo.Application.Utils;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Application.Handlers;

public class BoardHandler : IRequestHandler<CreateBoardCommand, Board>, IRequestHandler<DeleteBoardCommand, string>,
    IRequestHandler<EditBoardCommand, Board>, IRequestHandler<AddBoardParticipantCommand, string>,
    IRequestHandler<RemoveBoardParticipantCommand, string>,
    IRequestHandler<ConfirmBoardParticipantCommand, string>,
    IRequestHandler<GetAllBoardsQuery, PaginatedResult<Board>>,
    IRequestHandler<GetBoardByIdQuery, Board?>
{
    private readonly IBoardRepository _boardRepository;
    private readonly IInviteRepository _inviteRepository;
    private readonly IMailer _mailer;

    public BoardHandler(IBoardRepository boardRepository, IInviteRepository inviteRepository, IMailer mailer)
    {
        _inviteRepository = inviteRepository;
        _boardRepository = boardRepository;
        _mailer = mailer;
    }

    public async Task<Board> Handle(CreateBoardCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var board = new Board(command.Name, command.Description, command.User.Id)
        {
            Participants = new List<User> { command.User }
        };

        board.Columns.Add(new Column("Aberto", board.Id, 0));
        board.Columns.Add(new Column("Em Andamento", board.Id, 1));
        board.Columns.Add(new Column("Concluído", board.Id, 2));

        await _boardRepository.Create(board);

        return board;
    }

    public async Task<string> Handle(DeleteBoardCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var board = await _boardRepository.GetById(command.BoardId);
        if (board == null)
        {
            throw new NotFoundException("Quadro não encontrado!");
        }

        if (board.Owner != command.User)
        {
            throw new PermissionException("Sem permissão para deletar o quadro!");
        }

        await _boardRepository.Delete(board);

        return "Quadro deletado com sucesso!";
    }

    public async Task<Board> Handle(EditBoardCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var board = await _boardRepository.GetById(command.BoardId);
        if (board == null)
        {
            throw new NotFoundException("Quadro não encontrado!");
        }

        if (!board.UserCanEdit(command.User.Id))
        {
            throw new PermissionException("Sem permissão para alterar o quadro");
        }

        if (command.Name != null)
        {
            board.Name = command.Name;
        }

        if (command.Description != null)
        {
            board.Description = command.Description;
        }

        await _boardRepository.Update(board);
        
        return board;
    }

    public async Task<string> Handle(AddBoardParticipantCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var board = await _boardRepository.GetById(command.BoardId);
        if (board == null)
        {
            throw new NotFoundException("Quadro não encontrado!");
        }

        if (board.OwnerId != command.User.Id)
        {
            throw new PermissionException("Sem permissão para convidar usuários!");
        }

        var invites = new List<Invite>();
        foreach (var email in command.Emails)
        {
            invites.Add(new Invite(email, command.BoardId));
            await _mailer.SendMail(email,
                $"Você foi convidado para participar do quadro: {board.Name}, clique <a href='{command.Domain}/invite/{board.Id}'>aqui</a> para participar!");
        }

        await _inviteRepository.CreateMany(invites);

        return "Convites enviados com sucesso!";
    }

    public async Task<string> Handle(ConfirmBoardParticipantCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var invite = await _inviteRepository.GetInvite(command.User.Email, command.BoardId);
        if (invite == null)
        {
            throw new NotFoundException("Convite não encontrado!");
        }

        var board = await _boardRepository.GetById(command.BoardId);
        if (board == null)
        {
            throw new NotFoundException("Quadro não encontrado!");
        }

        board.Participants.Add(command.User);
        await _boardRepository.Update(board);

        return "Participante adicionado com sucesso!";
    }

    public async Task<string> Handle(RemoveBoardParticipantCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var board = await _boardRepository.GetById(command.BoardId);
        if (board == null)
        {
            throw new NotFoundException("Quadro não encontrado!");
        }

        if (board.Owner != command.User)
        {
            throw new PermissionException("Sem permissão para remover participantes!");
        }

        var participant = board.Participants.Find(x => x.Id == command.ParticipantId);
        if (participant == null)
        {
            throw new NotFoundException( "Participante não encontrado!");
        }

        board.Participants.Remove(participant);
        await _boardRepository.Update(board);

        return "Participante removido com sucesso!";
    }

    public async Task<PaginatedResult<Board>> Handle(GetAllBoardsQuery query, CancellationToken cancellationToken)
    {
        var boards = await _boardRepository.GetAll(query.User.Id, query.Page);

        return boards;
    }

    public async Task<Board?> Handle(GetBoardByIdQuery query, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetById(query.BoardId);
        
        if (board == null || !board.Participants.Contains(query.User))
        {
            throw new NotFoundException("Quadro não encontrado!");
        }

        return board;
    }
}