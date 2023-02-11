using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.Repositories;
using Todo.Domain.UseCases;
using Todo.Domain.Utils;

namespace Todo.Api.Controllers;

[ApiController, Route("auth")]
public class AuthController : TodoBaseController
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic Login([FromBody] LoginDTO data, [FromServices] IUserRepository repository, [FromServices] IHasher hasher, [FromServices] ITokenService tokenService)
    {
        var result = new LoginUseCase(repository, hasher).Handle(data);

        if (result.Code != 200)
        {
            return ParseResult(result);
        }

        if (result.Result == null)
        {
            return NotFound();
        }

        return tokenService.GenerateToken(result.Result);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(TokenResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic Register([FromBody] UserCreateDTO data, [FromServices] IUserRepository repository, [FromServices] IHasher hasher, [FromServices] ITokenService tokenService)
    {
        var result = new UserRegisterUseCase(repository, hasher).Handle(data);

        if (result.Code != 200)
        {
            return ParseResult(result);
        }

        if (result.Result == null)
        {
            return NotFound();
        }

        return tokenService.GenerateToken(result.Result);
    }
}
