using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface ITodoItemRepostory
{
    public TodoItem GetById(Guid id);
    public void Create(TodoItem item);
    public void Update(TodoItem item);
    public void Delete(Guid id);
}
