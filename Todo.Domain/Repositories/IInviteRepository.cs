using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IInviteRepository
{
    public Invite? GetInvite(string email, Guid boardId);
    public void CreateMany(ICollection<Invite> invite);
    public void Delete(Invite invite);
}
