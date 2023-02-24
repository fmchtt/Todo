using Todo.Domain.DTO;
using Todo.Domain.DTO.Output;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases.ItemUseCases;

public class ChangeItemColumnUseCase
{
    private readonly ITodoItemRepostory _todoRepository;
    private readonly IColumnRepository _columnRepository;

    public ChangeItemColumnUseCase(ITodoItemRepostory itemRepostory, IColumnRepository columnRepository)
    {
        _todoRepository = itemRepostory;
        _columnRepository = columnRepository;
    }

    public ResultDTO<TodoItemResultDTO> Handle(Guid columnId, Guid todoId, User user)
    {
        var todo = _todoRepository.GetById(todoId);
        var column = _columnRepository.GetById(columnId);

        if (todo == null || column == null)
        {
            return new ResultDTO<TodoItemResultDTO>(404, "Tarefa ou coluna não encontrados!");
        }

        if (column.Board.Participants.Find(x => x.Id == user.Id) == null)
        {
            return new ResultDTO<TodoItemResultDTO>(401, "Sem permissão para alterar a tarefa!");
        }

        todo.ChangeColumn(column);
        _todoRepository.Update(todo);

        return new ResultDTO<TodoItemResultDTO>(200, "Tarefa alterar com sucesso!", new TodoItemResultDTO(todo));
    }
}
