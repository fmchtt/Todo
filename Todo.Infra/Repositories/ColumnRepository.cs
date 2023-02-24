using Microsoft.EntityFrameworkCore;
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

    public void ColumnReorder(Guid boardId)
    {
        var columns = _dbContext.Columns.Where(x => x.BoardId == boardId).OrderBy(x => x.Order).ToList();
        for (var i = 0; i < columns.Count; i++)
        {
            columns[i].Order = i;
        }

        _dbContext.UpdateRange(columns);
    }

    public void ColumnReorder(Guid boardId, Guid columnId, int order)
    {
        var columns = _dbContext.Columns.Where(x => x.BoardId == boardId).OrderBy(x => x.Order).ToList();
        var columnIdx = columns.FindIndex(x => x.Id == columnId);

        var oldColumn = columns[columnIdx];
        columns.Remove(oldColumn);

        for (var i = 0; i < columns.Count; i++)
        {
            if (i < order)
            {
                columns[i].Order = i;
            }
            else
            {
                columns[i].Order = i + 1;
            }
        }
        columns.Add(oldColumn);

        _dbContext.UpdateRange(columns);
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

    public Column? GetById(Guid id)
    {
        return _dbContext.Columns.Include(x => x.Board.Participants).FirstOrDefault(x => x.Id == id);
    }

    public int GetMaxOrder(Guid boardId)
    {
        return _dbContext.Columns.Where(x => x.BoardId == boardId).Max(x => x.Order) + 1;
    }

    public void Update(Column column)
    {
        _dbContext.Columns.Update(column);
        _dbContext.SaveChanges();
    }
}
