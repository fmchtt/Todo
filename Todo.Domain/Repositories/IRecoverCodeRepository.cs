using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IRecoverCodeRepository
{
    public Task<RecoverCode?> Get(int code, string email);
    public Task<RecoverCode?> Get(string email);
    public Task Create(RecoverCode recoverCode);
    public Task Delete(RecoverCode recoverCode);
}
