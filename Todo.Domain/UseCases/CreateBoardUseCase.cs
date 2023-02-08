using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class CreateBoardUseCase
{
    private readonly IBoardRepository _boardRepository;

    public CreateBoardUseCase(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public ResultDTO<BoardResultDTO> Handle(CreateBoardDTO data, User user)
    {
        var board = new Board(data.Name, user.Id);
        board.Participants.Add(user);
        _boardRepository.CreateBoard(board);

        return new ResultDTO<BoardResultDTO>(201, "Quadro criado com sucesso!", new BoardResultDTO(board));
    }
}
