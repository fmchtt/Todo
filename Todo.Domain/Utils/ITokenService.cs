using Todo.Domain.Entities;

namespace Todo.Domain.Utils;

public interface ITokenService
{
    public string GenerateToken(User user);
}