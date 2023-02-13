using Todo.Domain.DTO;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class RemoveParticipantUseCase
{
    private readonly IBoardRepository _boardRepository;

    public RemoveParticipantUseCase(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public ResultDTO Handle(Guid BoardId, Guid ParticipantId, User user)
    {
        var board = _boardRepository.GetById(BoardId);
        if (board == null)
        {
            return new ResultDTO(404, "Quadro não encontrado!");
        }

        if (board.Owner != user)
        {
            return new ResultDTO(401, "Sem permissão para remover participantes!");
        }

        var participant = board.Participants.Find(x => x.Id == ParticipantId);
        if (participant == null)
        {
            return new ResultDTO(404, "Participante não encontrado!");
        }

        board.Participants.Remove(participant);
        _boardRepository.Update(board);

        return new ResultDTO(200, "Participante removido com sucesso!");
    }
}
