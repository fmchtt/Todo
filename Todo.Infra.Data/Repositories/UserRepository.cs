using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infra.Data.Contexts;

namespace Todo.Infra.Data.Repositories;

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

    public User? GetByEmail(string email)
    {
        return _dbContext.Users.FirstOrDefault(x => x.Email == email);
    }

    public User? GetById(Guid id)
    {
        return _dbContext.Users.FirstOrDefault(x => x.Id == id);
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();
    }
}
