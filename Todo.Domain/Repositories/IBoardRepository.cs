using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IBoardRepository
{
    public Board GetById(Guid id);
    public void Create(Board board);
    public void Update(Board board);
    public void Delete(Board board);
}
