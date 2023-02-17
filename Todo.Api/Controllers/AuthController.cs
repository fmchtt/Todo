using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Repositories;
using Todo.Domain.UseCases;
using Todo.Domain.Utils;

namespace Todo.Api.Controllers;

[ApiController, Route("auth")]
public class AuthController : TodoBaseController
{
    [HttpGet("me"), Authorize]
    [ProducesResponseType(typeof(UserResumedResultDTO), 200)]
    public dynamic Me()
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        return new UserResumedResultDTO(user);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic Login(
        [FromBody] LoginDTO data, 
        [FromServices] IUserRepository repository, 
        [FromServices] IHasher hasher, 
        [FromServices] ITokenService tokenService
    )
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
    public dynamic Register(
        [FromBody] UserCreateDTO data, 
        [FromServices] IUserRepository repository, 
        [FromServices] IHasher hasher, 
        [FromServices] ITokenService tokenService
    )
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

    [HttpPost("password/reset")]
    [ProducesResponseType(typeof(MessageResult), 200)]
    public dynamic PasswordReset(
        [FromBody] RecoverPasswordDTO data,
        [FromServices] IRecoverCodeRepository recoverCodeRepository,
        [FromServices] IUserRepository userRepository,
        [FromServices] IMailer mailer
    )
    {
        _ = new RecoverPasswordUseCase(mailer, userRepository, recoverCodeRepository).Handle(data);

        return new MessageResult("Caso o email esteja registrado, um código será enviado ao email");
    }

    [HttpPost("password/reset/verify")]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public MessageResult VerifyCode(
        [FromBody] CodeVerifyDTO data,
        [FromServices] IRecoverCodeRepository codeRepository
    )
    {
        var foundCode = codeRepository.Get(data.Code, data.Email);
        if (foundCode == null) {
            return Ok(new MessageResult("Código encontrado!"));
        }

        return NotFound(new MessageResult("Código não encontrado!"));
    }


    [HttpPost("password/reset/confirm")]
    [ProducesResponseType(typeof(MessageResult), 200)]
    public dynamic PasswordResetConfirm(
        [FromBody] ConfirmRecoverPasswordDTO data,
        [FromServices] IRecoverCodeRepository recoverCodeRepository,
        [FromServices] IUserRepository userRepository,
        [FromServices] IHasher hasher
    )
    {
        var result = new ConfirmRecoverPasswordUseCase(recoverCodeRepository, hasher, userRepository).Handle(data);

        return ParseResult(result);
    }
}
