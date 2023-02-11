using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infra.Contexts;

namespace Todo.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TodoDBContext _dbContext;

    public UserRepository(TodoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
    }

    public User GetByEmail(string email)
    {
        return _dbContext.Users.Where(x => x.Email == email).First();
    }

    public User GetById(Guid id)
    {
        return _dbContext.Users.Where(x => x.Id == id).First();
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();
    }
}
