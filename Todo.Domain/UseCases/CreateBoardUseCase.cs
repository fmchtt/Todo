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
        board.Participants = new List<User> { user };
        board.Columns.Add(new Column("Aberto", board.Id));
        board.Columns.Add(new Column("Em Andamento", board.Id));
        board.Columns.Add(new Column("Concluído", board.Id));

        _boardRepository.Create(board);

        return new ResultDTO<BoardResultDTO>(201, "Quadro criado com sucesso!", new BoardResultDTO(board));
    }
}
