using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Domain.Repositories;

public interface ITodoItemRepository
{
    public Task<TodoItem?> GetById(Guid id);
    public Task<PaginatedResult<TodoItem>> GetAll(Guid ownerId, int page);
    public Task<List<TodoItem>> GetAllByTitle(string title, Guid ownerId);
    public Task Create(TodoItem item);
    public Task Update(TodoItem item);
    public Task Delete(TodoItem item);
}
