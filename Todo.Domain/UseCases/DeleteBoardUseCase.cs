using Todo.Domain.DTO;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class DeleteBoardUseCase
{
    private readonly IBoardRepository _boardRepository;

    public DeleteBoardUseCase(IBoardRepository boardRepository) { 
        _boardRepository = boardRepository; 
    }

    public ResultDTO Handle(Guid BoardId, User user)
    {
        var board = _boardRepository.GetById(BoardId);
        if (board == null)
        {
            return new ResultDTO(404, "Quadro não encontrado!");
        }

        if (board.Owner != user)
        {
            return new ResultDTO(401, "Sem permissão para deletar o quadro!");
        }

        _boardRepository.Delete(board);

        return new ResultDTO(200, "Quadro apagado com sucesso!");
    }
}
