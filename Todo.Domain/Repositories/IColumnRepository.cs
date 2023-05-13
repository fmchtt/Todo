using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IColumnRepository
{
    public Task<Column?> GetById(Guid id);
    public Task<int> GetMaxOrder(Guid boardId);
    public Task Create(Column column);
    public Task Update(Column column);
    public Task ColumnReorder(Guid boardId);
    public Task ColumnReorder(Guid boardId, Guid columnId, int order);
    public Task Delete(Column column);
}
