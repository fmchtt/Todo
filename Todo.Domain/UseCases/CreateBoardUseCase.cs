using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class CreateBoardUseCase
{
    private readonly IBoardRepository _boardRepository;
    private readonly IColumnRepository _columnRepository;

    public CreateBoardUseCase(IBoardRepository boardRepository, IColumnRepository columnRepository)
    {
        _boardRepository = boardRepository;
        _columnRepository = columnRepository;
    }

    public ResultDTO<BoardResultDTO> Handle(CreateBoardDTO data, User user)
    {
        var board = new Board(data.Name, user.Id);
        board.Participants = new List<User> { user };

        _boardRepository.Create(board);

        _columnRepository.Create(new Column("Aberto", board.Id));
        _columnRepository.Create(new Column("Em Andamento", board.Id));
        _columnRepository.Create(new Column("Concluído", board.Id));

        return new ResultDTO<BoardResultDTO>(201, "Quadro criado com sucesso!", new BoardResultDTO(board));
    }
}
