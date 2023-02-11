using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IColumnRepository
{
    public Column? GetById(Guid id);
    public void Create(Column column);
    public void Update(Column column);
    public void Delete(Column column);
}
