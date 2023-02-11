using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface ITodoItemRepostory
{
    public TodoItem GetById(Guid id);
    public List<TodoItem> GetAll(Guid ownerId);
    public List<TodoItem> GetAllByTitle(string title, Guid ownerId);
    public void Create(TodoItem item);
    public void Update(TodoItem item);
    public void Delete(TodoItem item);
}
