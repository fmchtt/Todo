using Todo.Domain.Entities;

namespace Todo.Application.Utils;

public interface ITokenService
{
    public string GenerateToken(User user);
}