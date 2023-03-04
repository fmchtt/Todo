using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Domain.Repositories;

public interface ITodoItemRepository
{
    public TodoItem? GetById(Guid id);
    public PaginatedResult<TodoItem> GetAll(Guid ownerId, int page);
    public List<TodoItem> GetAllByTitle(string title, Guid ownerId);
    public void Create(TodoItem item);
    public void Update(TodoItem item);
    public void Delete(TodoItem item);
}
