using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IUserRepository
{
    User GetById(Guid id);
    User GetByEmail(string email);
    void Create(User user);
    void Update(User user);
    void Delete(Guid id);
}
