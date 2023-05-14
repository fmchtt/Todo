using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Domain.Repositories;

public interface IBoardRepository
{
    public Task<Board?> GetById(Guid id);
    public Task<PaginatedResult<Board>> GetAll(Guid ownerId, int page);
    public Task<List<Board>> GetAllByName(string name, Guid ownerId);
    public Task Create(Board board);
    public Task Update(Board board);
    public Task Delete(Board board);
}
