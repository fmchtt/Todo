using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infra.Contexts;

namespace Todo.Infra.Repositories;

public class TodoItemRepository : ITodoItemRepostory
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

    public List<TodoItem> GetAll(Guid ownerId)
    {
        return _dbContext.Itens.Where(x => x.CreatorId == ownerId).OrderByDescending(x => x.CreatedDate).ToList();
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
