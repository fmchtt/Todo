using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Domain.Repositories;

public interface ICommentRepository
{
    public Task<Comment?> GetById(Guid id);
    public Task<PaginatedResult<Comment>> GetAllByItemId(Guid itemId, int page);
    public Task Update(Comment comment);
    public Task Create(Comment comment);
    public Task Delete(Comment comment);
}