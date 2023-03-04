using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Domain.Repositories;

public interface IBoardRepository
{
    public Board? GetById(Guid id);
    public PaginatedResult<Board> GetAll(Guid ownerId, int page);
    public List<Board> GetAllByName(string name, Guid ownerId);
    public void Create(Board board);
    public void Update(Board board);
    public void Delete(Board board);
}
