using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infra.Contexts;

namespace Todo.Infra.Repositories;

public class ColumnRepository : IColumnRepository
{
    private readonly TodoDBContext _dbContext;

    public ColumnRepository(TodoDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    public void Create(Column column)
    {
        _dbContext.Columns.Add(column);
        _dbContext.SaveChanges();
    }

    public void Delete(Column column)
    {
        _dbContext.Columns.Remove(column);
        _dbContext.SaveChanges();
    }

    public Column GetById(Guid id)
    {
        return _dbContext.Columns.Where(x => x.Id == id).First();
    }

    public void Update(Column column)
    {
        _dbContext.Columns.Update(column);
        _dbContext.SaveChanges();
    }
}
