using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IInviteRepository
{
    public Task<Invite?> GetInvite(string email, Guid boardId);
    public Task CreateMany(ICollection<Invite> invite);
    public Task Delete(Invite invite);
}
