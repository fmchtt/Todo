using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Domain.Results;
using Todo.Infra.Data.Contexts;

namespace Todo.Infra.Data.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly TodoDBContext _dbContext;

    public TodoItemRepository(TodoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(TodoItem item)
    {
        _dbContext.Itens.Add(item);
        _dbContext.SaveChanges();
    }

    public void Delete(TodoItem item)
    {
        _dbContext.Itens.Remove(item);
        _dbContext.SaveChanges();
    }

    public PaginatedResult<TodoItem> GetAll(Guid ownerId, int page)
    {
        var offset = page * 10;

        var query = _dbContext.Itens.Where(x => x.CreatorId == ownerId).Skip(offset).Take(10).OrderByDescending(x => x.CreatedDate);
        
        var results = query.ToList();
        var pageCount = query.Count() / 10;

        return new PaginatedResult<TodoItem>(results, pageCount);
    }

    public List<TodoItem> GetAllByTitle(string title, Guid ownerId)
    {
        return _dbContext.Itens.Where(x => x.CreatorId == ownerId && x.Title.Contains(title)).ToList();
    }

    public TodoItem? GetById(Guid id)
    {
        return _dbContext.Itens.FirstOrDefault(x => x.Id == id);
    }

    public void Update(TodoItem item)
    {
        _dbContext.Itens.Update(item);
        _dbContext.SaveChanges();
    }
}
