using Todo.Domain.DTO;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases.ItemUseCases;

public class DeleteTodoItemUseCase
{
    private readonly ITodoItemRepostory _todoItemRepostory;

    public DeleteTodoItemUseCase(ITodoItemRepostory todoItemRepostory)
    {
        _todoItemRepostory = todoItemRepostory;
    }

    public ResultDTO Handle(Guid ItemId, User user)
    {
        var item = _todoItemRepostory.GetById(ItemId);
        if (item == null)
        {
            return new ResultDTO(404, "Tarefa não encontrada!");
        }

        if (item.Board != null && item.Board.Participants.Find(x => x.Id == user.Id) == null || item.Creator != user)
        {
            return new ResultDTO(401, "Sem permissão para apagar a tarefa!");
        }

        _todoItemRepostory.Delete(item);

        return new ResultDTO(200, "Tarefa apagada com sucesso!");
    }
}
