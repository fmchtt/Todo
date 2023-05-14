using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infra.Data.Contexts;

namespace Todo.Infra.Data.Repositories;

public class RecoverCodeRepository : IRecoverCodeRepository
{
    private readonly TodoDBContext _dbContext;

    public RecoverCodeRepository(TodoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(RecoverCode recoverCode)
    {
        await _dbContext.RecoverCodes.AddAsync(recoverCode);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(RecoverCode recoverCode)
    {
        _dbContext.RecoverCodes.Remove(recoverCode); 
        await _dbContext.SaveChangesAsync();
    }

    public async Task<RecoverCode?> Get(int code, string email)
    {
        return await _dbContext.RecoverCodes.FirstAsync(x => x.Code == code && x.User.Email == email);
    }

    public  async Task<RecoverCode?> Get(string email)
    {
        return await _dbContext.RecoverCodes.FirstAsync(x => x.User.Email == email);
    }
}
