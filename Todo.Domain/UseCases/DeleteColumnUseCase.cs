using Todo.Domain.DTO;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class DeleteColumnUseCase
{
    private readonly IColumnRepository _columnRepository;

    public DeleteColumnUseCase(IColumnRepository columnRepository)
    {
        _columnRepository = columnRepository;
    }

    public ResultDTO Handle(Guid ColumnId, User user)
    {
        var column = _columnRepository.GetById(ColumnId);
        if (column == null)
        {
            return new ResultDTO(404, "Coluna não encontrada!");
        }

        if (column.Board.Participants.Find(x => x.Id == user.Id) == null)
        {
            return new ResultDTO(401, "Sem permissão para apagar a coluna!");
        }

        _columnRepository.Delete(column);

        return new ResultDTO(200, "Coluna deletada com sucesso!");
    }
}
