using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Domain.Results;
using Todo.Infra.Data.Contexts;
using Todo.Infra.Data.Extensions;

namespace Todo.Infra.Data.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly TodoDBContext _dbContext;

    public CommentRepository(TodoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Comment?> GetById(Guid id)
    {
        return await _dbContext.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
    }

    public async Task<PaginatedResult<Comment>> GetAllByItemId(Guid itemId, int page)
    {
        var query = _dbContext.Comments.Where(comment => comment.ItemId == itemId).OrderByDescending(x => x.CreationTimeStamp).GetPage(page, 10);
        var pageCount = query.Count() / 10;
        var results = await query.ToListAsync();
        return new PaginatedResult<Comment>(results, pageCount);
    }

    public async Task Update(Comment comment)
    {
        _dbContext.Update(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Create(Comment comment)
    {
        await _dbContext.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Comment comment)
    {
        _dbContext.Remove(comment);
        await _dbContext.SaveChangesAsync();
    }
}