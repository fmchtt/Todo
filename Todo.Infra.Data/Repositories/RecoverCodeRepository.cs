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

    public void Create(RecoverCode recoverCode)
    {
        _dbContext.RecoverCodes.Add(recoverCode);
        _dbContext.SaveChanges();
    }

    public void Delete(RecoverCode recoverCode)
    {
        _dbContext.RecoverCodes.Remove(recoverCode); 
        _dbContext.SaveChanges();
    }

    public RecoverCode Get(int code, string email)
    {
        return _dbContext.RecoverCodes.First(x => x.Code == code && x.User.Email == email);
    }

    public RecoverCode Get(string email)
    {
        return _dbContext.RecoverCodes.First(x => x.User.Email == email);
    }
}
