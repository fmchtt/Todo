using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IBoardRepository
{
    public Board GetById(Guid id);
    public void CreateBoard(Board board);
    public void UpdateBoard(Board board);
    public void DeleteBoard(Guid id);
}
