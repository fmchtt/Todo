using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class CreateTodoItemUseCase
{
    private readonly ITodoItemRepostory _todoItemRepostory;

    public CreateTodoItemUseCase(ITodoItemRepostory todoItemRepostory)
    {
        _todoItemRepostory = todoItemRepostory;
    }

    public ResultDTO<TodoItemResultDTO> Handle(CreateTodoItemDTO data, User user, Guid boardId)
    {
        var todoItem = new TodoItem(data.Title, data.Description, boardId, user.Id, false, null);
        _todoItemRepostory.Create(todoItem);

        return new ResultDTO<TodoItemResultDTO>(201, "Item criado com sucesso!", new TodoItemResultDTO(todoItem));
    }
}
