using Todo.Api.DTO;
using Todo.Domain.Entities;

namespace Todo.Api.Contracts;

public interface ITokenService
{
    public TokenResult GenerateToken(User user);
}
