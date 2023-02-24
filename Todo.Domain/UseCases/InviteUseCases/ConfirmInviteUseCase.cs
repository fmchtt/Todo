using Todo.Domain.DTO;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases.InviteUseCases;

public class ConfirmInviteUseCase
{
    private readonly IInviteRepository _inviteRepository;
    private readonly IBoardRepository _boardRepository;
    
    public ConfirmInviteUseCase(IInviteRepository inviteRepository, IBoardRepository boardRepository)
    {
        _inviteRepository = inviteRepository;
        _boardRepository = boardRepository;
    }

    public ResultDTO Handle(Guid boardId, User user)
    {
        var invite = _inviteRepository.GetInvite(user.Email, boardId);
        if (invite == null)
        {
            return new ResultDTO(404, "Convite não encontrado!");
        }

        var board = _boardRepository.GetById(boardId);
        if (board == null)
        {
            return new ResultDTO(404, "Quadro não encontrado!");
        }

        board.Participants.Add(user);
        _boardRepository.Update(board);

        return new ResultDTO(201, "Participante adicionado com sucesso!");
    }
}
