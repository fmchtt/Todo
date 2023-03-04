using Todo.Domain.Commands.BoardCommands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;
using Todo.Domain.Utils;

namespace Todo.Domain.Handlers;

public class BoardHandler : IHandler<CreateBoardCommand, Board>, IHandler<DeleteBoardCommand>, IHandler<EditBoardCommand, Board> , IHandler<AddBoardParticipantCommand>, IHandler<RemoveBoardParticipantCommand>, IHandler<ConfirmBoardParticipantCommand>
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
    
    public CommandResult<Board> Handle(CreateBoardCommand command, User user)
    {
        var board = new Board(command.Name, command.Description, user.Id)
        {
            Participants = new List<User> { user }
        };

        board.Columns.Add(new Column("Aberto", board.Id, 0));
        board.Columns.Add(new Column("Em Andamento", board.Id, 1));
        board.Columns.Add(new Column("Concluído", board.Id, 2));

        _boardRepository.Create(board);

        return new CommandResult<Board>(Code.Created, "Quadro criado com sucesso!", board);
    }

    public CommandResult Handle(DeleteBoardCommand command, User user)
    {
        var board = _boardRepository.GetById(command.BoardId);
        if (board == null)
        {
            return new CommandResult(Code.NotFound, "Quadro não encontrado!");
        }

        if (board.Owner != user)
        {
            return new CommandResult(Code.Unauthorized, "Sem permissão para deletar o quadro!");
        }

        _boardRepository.Delete(board);

        return new CommandResult(Code.Ok, "Quadro apagado com sucesso!");
    }

    public CommandResult<Board> Handle(EditBoardCommand command, User user)
    {
        var board = _boardRepository.GetById(command.BoardId);
        if (board == null)
        {
            return new CommandResult<Board>(Code.NotFound, "Quadro não encontrado!");
        }

        if (!board.UserCanEdit(user.Id))
        {
            return new CommandResult<Board>(Code.Unauthorized, "Sem permissão para alterar o quadro");
        }

        if (command.Name != null)
        {
            board.Name = command.Name;
        }

        if (command.Description != null)
        {
            board.Description = command.Description;
        }

        _boardRepository.Update(board);
        

        return new CommandResult<Board>(Code.Ok, "Quadro editado com sucesso!", board);
    }

    public CommandResult Handle(AddBoardParticipantCommand command, User user)
    {
        var board = _boardRepository.GetById(command.BoardId);
        if (board == null) {
            return new CommandResult(Code.NotFound, "Quadro não encontrado!");
        }

        if (board.OwnerId != user.Id)
        {
            return new CommandResult(Code.Unauthorized, "Sem permissão para convidar usuários!");
        }

        var invites = new List<Invite>();
        foreach (var email in command.Emails)
        {
            invites.Add(new Invite(email, command.BoardId));
            _mailer.SendMail(email, $"Você foi convidado para participar do quadro: {board.Name}, clique <a href='{command.Domain}/invite/{board.Id}'>aqui</a> para participar!");
        }
        _inviteRepository.CreateMany(invites);

        return new CommandResult(Code.Created, "Convites criados com sucesso!");
    }

    public CommandResult Handle(ConfirmBoardParticipantCommand command, User user)
    {
        var invite = _inviteRepository.GetInvite(user.Email, command.BoardId);
        if (invite == null)
        {
            return new CommandResult(Code.NotFound, "Convite não encontrado!");
        }

        var board = _boardRepository.GetById(command.BoardId);
        if (board == null)
        {
            return new CommandResult(Code.NotFound, "Quadro não encontrado!");
        }

        board.Participants.Add(user);
        _boardRepository.Update(board);

        return new CommandResult(Code.Created, "Participante adicionado com sucesso!");
    }

    public CommandResult Handle(RemoveBoardParticipantCommand command, User user)
    {
        var board = _boardRepository.GetById(command.BoardId);
        if (board == null)
        {
            return new CommandResult(Code.NotFound, "Quadro não encontrado!");
        }

        if (board.Owner != user)
        {
            return new CommandResult(Code.Unauthorized, "Sem permissão para remover participantes!");
        }

        var participant = board.Participants.Find(x => x.Id == command.ParticipantId);
        if (participant == null)
        {
            return new CommandResult(Code.NotFound, "Participante não encontrado!");
        }

        board.Participants.Remove(participant);
        _boardRepository.Update(board);

        return new CommandResult(Code.Ok, "Participante removido com sucesso!");
    }
}