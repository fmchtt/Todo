using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class CreateTodoItemUseCase
{
    private readonly ITodoItemRepostory _todoItemRepostory;
    private readonly IBoardRepository _boardRepository;
    private readonly IColumnRepository _columnRepository;

    public CreateTodoItemUseCase(ITodoItemRepostory todoItemRepostory, IBoardRepository boardRepository, IColumnRepository columnRepository)
    {
        _todoItemRepostory = todoItemRepostory;
        _boardRepository = boardRepository;
        _columnRepository = columnRepository;
    }

    public ResultDTO<TodoItemResultDTO> Handle(CreateTodoItemDTO data, User user)
    {
        if (data.BoardId.HasValue)
        {
            var board = _boardRepository.GetById(data.BoardId.Value);

            if (board == null || !board.Participants.Exists(x => user.Id == x.Id)) {
                return new ResultDTO<TodoItemResultDTO>(401, "Usuário não autorizado a adicionar itens nesse quadro!");
            }
        }

        if (data.ColumnId.HasValue)
        {
            var column = _columnRepository.GetById(data.ColumnId.Value);

            if (column == null || !column.Board.Participants.Exists(x => user.Id == x.Id))
            {
                return new ResultDTO<TodoItemResultDTO>(401, "Usuário não autorizado a adicionar itens nessa coluna!");
            }
        }
        
        var todoItem = new TodoItem(data.Title, data.Description, data.BoardId, user.Id, false, data.Priority, null, data.ColumnId);
        _todoItemRepostory.Create(todoItem);

        return new ResultDTO<TodoItemResultDTO>(201, "Item criado com sucesso!", new TodoItemResultDTO(todoItem));
    }
}
