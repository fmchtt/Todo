using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Domain.Results;
using Todo.Infra.Data.Contexts;

namespace Todo.Infra.Data.Repositories;

public class BoardRepository : IBoardRepository
{
    private readonly TodoDBContext _dbContext;

    public BoardRepository(TodoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Board board)
    {
        await _dbContext.Boards.AddAsync(board);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Board board)
    {
        _dbContext.Boards.Remove(board); 
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PaginatedResult<Board>> GetAll(Guid ownerId, int page)
    {
        var offset = (page - 1) * 10;
        var user = await _dbContext.Users.FirstAsync(x => x.Id == ownerId);

        var query = _dbContext.Boards.Where(x => x.Participants.Contains(user)).OrderBy(x => x.Name);

        var result = await query.Skip(offset).Take(10).ToListAsync();
        var count = query.Count() / 10;

        return new PaginatedResult<Board>(result, count);
    }

    public async Task<List<Board>> GetAllByName(string name, Guid ownerId)
    {
        return await _dbContext.Boards.Where(x => x.OwnerId == ownerId && x.Name.Contains(name)).ToListAsync();
    }

    public async Task<Board?> GetById(Guid id)
    {
        return await _dbContext.Boards.Include(x => x.Participants).Include(x => x.Columns).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(Board board)
    {
        _dbContext.Boards.Update(board);
        await _dbContext.SaveChangesAsync();
    }
}
