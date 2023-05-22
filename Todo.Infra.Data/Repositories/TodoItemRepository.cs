using Microsoft.EntityFrameworkCore;
using Todo.Domain.Results;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infra.Data.Contexts;
using Todo.Infra.Data.Extensions;

namespace Todo.Infra.Data.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
  private readonly TodoDBContext _dbContext;

  public TodoItemRepository(TodoDBContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task Create(TodoItem item)
  {
    await _dbContext.Itens.AddAsync(item);
    await _dbContext.SaveChangesAsync();
  }

  public async Task Delete(TodoItem item)
  {
    _dbContext.Itens.Remove(item);
    await _dbContext.SaveChangesAsync();
  }

  public async Task<PaginatedResult<TodoItem>> GetAll(Guid ownerId, int page)
  {
    var query = _dbContext.Itens.Where(x => x.CreatorId == ownerId).OrderByDescending(x => x.CreatedDate);

    var results = await query.GetPage(page, 10).ToListAsync();
    var pageCount = query.Count() / 10;

    return new PaginatedResult<TodoItem>(results, pageCount);
  }

  public async Task<List<TodoItem>> GetAllByTitle(string title, Guid ownerId)
  {
    return await _dbContext.Itens.Where(x => x.CreatorId == ownerId && x.Title.Contains(title)).ToListAsync();
  }

  public async Task<TodoItem?> GetById(Guid id)
  {
    return await _dbContext.Itens.FirstOrDefaultAsync(x => x.Id == id);
  }

  public async Task Update(TodoItem item)
  {
    _dbContext.Itens.Update(item);
    await _dbContext.SaveChangesAsync();
  }
}
