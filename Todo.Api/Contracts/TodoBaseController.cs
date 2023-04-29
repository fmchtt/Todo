using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Api.Contracts;

public class TodoBaseController : ControllerBase
{
    [NonAction]
    protected IActionResult ParseResult<T, TR>(CommandResult<T> result)
    {
        var message = result.Message;
        var errors = result.Errors;
        var mapper = HttpContext.RequestServices.GetService<IMapper>();

        if (mapper == null)
        {
            return StatusCode(500, "Erro ao converter o resultado da operação");
        }

        var finalResult = result.Code switch
        {
            Code.Ok => result.Result != null ? Ok(mapper.Map<TR>(result.Result)) : Ok(message),
            Code.Created => result.Result != null
                ? StatusCode(201, mapper.Map<TR>(result.Result))
                : StatusCode(201, message),
            Code.NotFound => NotFound(message),
            Code.Invalid => errors != null ? BadRequest(errors) : BadRequest(message),
            Code.Unauthorized => Unauthorized(message),
            _ => StatusCode(500, "Erro ao converter o resultado da operação")
        };

        return finalResult;
    }

    [NonAction]
    protected dynamic ParseResult(CommandResult result)
    {
        var message = result.Message;
        var errors = result.Errors;

        return result.Code switch
        {
            Code.Ok => Ok(message),
            Code.Created => StatusCode(201, message),
            Code.NotFound => NotFound(message),
            Code.Invalid => errors != null ? BadRequest(errors) : BadRequest(message),
            Code.Unauthorized => Unauthorized(message),
            _ => StatusCode(500, "Erro ao converter o resultado da operação")
        };
    }

    [NonAction]
    protected Guid GetUserId()
    {
        var userId = User.Identity?.Name;
        return userId == null ? Guid.Empty : Guid.Parse(userId);
    }

    [NonAction]
    protected User GetUser()
    {
        var userRepository = HttpContext.RequestServices.GetService<IUserRepository>();
        if (userRepository == null)
        {
            throw new UnauthorizedAccessException("Usuário não encontrado!");
        }

        var userId = GetUserId();
        var user = userRepository.GetById(userId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Usuário não encontrado!");
        }

        return user;
    }
}