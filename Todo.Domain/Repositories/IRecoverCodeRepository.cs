using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IRecoverCodeRepository
{
    public RecoverCode Get(int code, string email);
    public RecoverCode Get(string email);
    public void Create(RecoverCode recoverCode);
    public void Delete(RecoverCode recoverCode);
}
