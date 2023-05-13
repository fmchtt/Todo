using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infra.Contexts;

namespace Todo.Infra.Repositories;

public class InviteRepository : IInviteRepository
{
    private readonly TodoDBContext _dbContext;

    public InviteRepository(TodoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateMany(ICollection<Invite> invite)
    {
        _dbContext.Invites.AddRange(invite);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Invite invite)
    {
        _dbContext.Invites.Remove(invite);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Invite?> GetInvite(string email, Guid boardId)
    {
        return await _dbContext.Invites.FirstOrDefaultAsync(x => x.Email == email && x.BoardId == boardId);
    }
}
