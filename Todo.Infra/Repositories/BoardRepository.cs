using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infra.Contexts;

namespace Todo.Infra.Repositories;

public class BoardRepository : IBoardRepository
{
    private readonly TodoDBContext _dbContext;

    public BoardRepository(TodoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(Board board)
    {
        _dbContext.Boards.Add(board);
        _dbContext.SaveChanges();
    }

    public void Delete(Board board)
    {
        _dbContext.Boards.Remove(board); 
        _dbContext.SaveChanges();
    }

    public List<Board> GetAll(Guid ownerId)
    {
        return _dbContext.Boards.Where(x => x.OwnerId == ownerId).ToList();
    }

    public List<Board> GetAllByName(string name, Guid ownerId)
    {
        return _dbContext.Boards.Where(x => x.OwnerId == ownerId && x.Name.Contains(name)).ToList();
    }

    public Board? GetById(Guid id)
    {
        return _dbContext.Boards.Include(x => x.Participants).Include(x => x.Columns).FirstOrDefault(x => x.Id == id);
    }

    public void Update(Board board)
    {
        _dbContext.Boards.Update(board);
        _dbContext.SaveChanges();
    }
}
