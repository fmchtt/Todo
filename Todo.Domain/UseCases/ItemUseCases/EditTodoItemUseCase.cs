using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases.ItemUseCases;

public class EditTodoItemUseCase
{
    private readonly ITodoItemRepostory _todoItemRepostory;

    public EditTodoItemUseCase(ITodoItemRepostory todoRepostory)
    {
        _todoItemRepostory = todoRepostory;
    }

    public ResultDTO<TodoItemResultDTO> Handle(EditTodoItemDTO data, Guid itemId, User user)
    {
        var item = _todoItemRepostory.GetById(itemId);
        if (item == null)
        {
            return new ResultDTO<TodoItemResultDTO>(404, "Tarefa não encontrada");
        }

        if (item.Board != null && item.Board.Participants.Find(x => x == user) == null && item.Creator != user)
        {
            return new ResultDTO<TodoItemResultDTO>(401, "Sem permissão para alterar a Tarefa");
        }

        foreach (var prop in data.GetType().GetProperties())
        {
            var value = prop.GetValue(data, null);
            if (value != null)
            {
                prop.SetValue(item, value);
            }
        }

        item.UpdatedDate = DateTime.UtcNow;
        _todoItemRepostory.Update(item);

        return new ResultDTO<TodoItemResultDTO>(200, "Tarefa editada com sucesso!", new TodoItemResultDTO(item));
    }
}
