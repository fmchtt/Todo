using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IUserRepository
{
    User? GetById(Guid id);
    Task<User?> GetByEmail(string email);
    Task Create(User user);
    Task Update(User user);
    Task Delete(User user);
}
