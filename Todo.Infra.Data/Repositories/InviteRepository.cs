using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infra.Data.Contexts;

namespace Todo.Infra.Data.Repositories;

public class InviteRepository : IInviteRepository
{
    private readonly TodoDBContext _dbContext;

    public InviteRepository(TodoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateMany(ICollection<Invite> invite)
    {
        _dbContext.Invites.AddRange(invite);
        _dbContext.SaveChanges();
    }

    public void Delete(Invite invite)
    {
        _dbContext.Invites.Remove(invite);
        _dbContext.SaveChanges();
    }

    public Invite? GetInvite(string email, Guid boardId)
    {
        return _dbContext.Invites.FirstOrDefault(x => x.Email == email && x.BoardId == boardId);
    }
}
