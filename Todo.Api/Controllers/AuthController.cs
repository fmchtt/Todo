using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.Commands.UserCommands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("auth")]
public class AuthController : TodoBaseController
{
    [HttpGet("me"), Authorize]
    [ProducesResponseType(typeof(ResumedUserResult), 200)]
    public dynamic Me([FromServices] IMapper mapper)
    {
        var user = GetUser();
        return mapper.Map<ResumedUserResult>(user);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic Login(
        LoginCommand command, 
        [FromServices] ITokenService tokenService,
        [FromServices] UserHandler handler
    )
    {
        var result = handler.Handle(command);

        if (result.Code != Code.Ok)
        {
            return ParseResult<User, ResumedUserResult>(result);
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
        [FromBody] RegisterCommand command, 
        [FromServices] UserHandler handler, 
        [FromServices] ITokenService tokenService
    )
    {
        var result = handler.Handle(command);

        if (result.Code != Code.Ok)
        {
            return ParseResult<User, ResumedUserResult>(result);
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
        RecoverPasswordCommand command,
        [FromServices] UserHandler handler
    )
    {
        handler.Handle(command);

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
        ConfirmRecoverPasswordCommand command,
        [FromServices] UserHandler handler
    )
    {
        var result = handler.Handle(command);

        return ParseResult(result);
    }
}
