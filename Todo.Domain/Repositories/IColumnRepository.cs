using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IColumnRepository
{
    public Column? GetById(Guid id);
    public int GetMaxOrder(Guid boardId);
    public void Create(Column column);
    public void Update(Column column);
    public void ColumnReorder(Guid boardId);
    public void ColumnReorder(Guid boardId, Guid columnId, int order);
    public void Delete(Column column);
}
