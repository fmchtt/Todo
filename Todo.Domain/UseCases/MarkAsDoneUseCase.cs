using Todo.Domain.DTO;
using Todo.Domain.DTO.Output;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class MarkAsDoneUseCase
{
    private readonly ITodoItemRepostory _itemRepository;

    public MarkAsDoneUseCase(ITodoItemRepostory itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public ResultDTO<TodoItemResultDTO> Handle(Guid itemId, User user)
    {
        var item = _itemRepository.GetById(itemId);
        if (item == null) {
            return new ResultDTO<TodoItemResultDTO>(404, "Tarefa não encontrada!");
        }

        if (!item.UserCandEdit(user.Id))
        {
            return new ResultDTO<TodoItemResultDTO>(401, "Sem permissão para alterar a Tarefa!");
        }

        item.MarkAsDone();
        _itemRepository.Update(item);

        return new ResultDTO<TodoItemResultDTO>(200, "Tarefa atualizada com sucesso!", new TodoItemResultDTO(item));
    }
}
