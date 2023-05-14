using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infra.Data.Contexts;

namespace Todo.Infra.Data.Repositories;

public class ColumnRepository : IColumnRepository
{
    private readonly TodoDBContext _dbContext;

    public ColumnRepository(TodoDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    public async Task ColumnReorder(Guid boardId)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        
        var columns = await _dbContext.Columns.Where(x => x.BoardId == boardId).OrderBy(x => x.Order).ToListAsync();

        for (var i = 0; i < columns.Count; i++)
        {
            columns[i].Order = columns.Count + (1 + i);
        }
        _dbContext.UpdateRange(columns);
        await _dbContext.SaveChangesAsync();
        
        for (var i = 0; i < columns.Count; i++)
        {
            columns[i].Order = i;
        }

        _dbContext.UpdateRange(columns);
        await _dbContext.SaveChangesAsync();
        
        await transaction.CommitAsync();
    }

    public async Task ColumnReorder(Guid boardId, Guid columnId, int order)
    {
        var columns = await _dbContext.Columns.Where(x => x.BoardId == boardId).OrderBy(x => x.Order).ToListAsync();

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        for (var i = 0; i < columns.Count; i++)
        {
            columns[i].Order = columns.Count + (1 + i);
        }
        _dbContext.UpdateRange(columns);
        await _dbContext.SaveChangesAsync();

        var columnIdx = columns.FindIndex(x => x.Id == columnId);

        var oldColumn = columns[columnIdx];
        oldColumn.Order = order;

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
        await _dbContext.SaveChangesAsync();
        
        await transaction.CommitAsync();
    }

    public async Task Create(Column column)
    {
        await _dbContext.Columns.AddAsync(column);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Column column)
    {
        _dbContext.Columns.Remove(column);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Column?> GetById(Guid id)
    {
        return await _dbContext.Columns.Include(x => x.Board.Participants).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> GetMaxOrder(Guid boardId)
    {
        return await _dbContext.Columns.Where(x => x.BoardId == boardId).MaxAsync(x => x.Order) + 1;
    }

    public async Task Update(Column column)
    {
        _dbContext.Columns.Update(column);
        await _dbContext.SaveChangesAsync();
    }
}