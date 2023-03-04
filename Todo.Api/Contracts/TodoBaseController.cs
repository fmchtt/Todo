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
    protected dynamic ParseResult<T, TR>(CommandResult<T> result)
    {
        var message = result.Message;
        var errors = result.Errors;
        var mapper = HttpContext.RequestServices.GetService<IMapper>();

        if (mapper == null)
        {
            return InternalServerError("Erro ao converter o resultado da operação");
        }

        return result.Code switch
        {
            Code.Ok => (result.Result != null ? Ok(mapper.Map<TR>(result.Result)) : Ok(message)),
            Code.Created => (result.Result != null ? Ok(mapper.Map<TR>(result.Result)) : Created(message)),
            Code.NotFound => NotFound(message),
            Code.Invalid => errors != null ? BadRequest(errors) : BadRequest(message),
            Code.Unauthorized => Unauthorized(message),
            _ => InternalServerError("Erro ao converter o resultado da operação")
        };
    }

    [NonAction]
    protected dynamic ParseResult(CommandResult result)
    {
        var message = result.Message;
        var errors = result.Errors;

        return result.Code switch
        {
            Code.Ok => Ok(message),
            Code.Created => Created(message),
            Code.NotFound => NotFound(message),
            Code.Invalid => errors != null ? BadRequest(errors) : BadRequest(message),
            Code.Unauthorized => Unauthorized(message),
            _ => InternalServerError("Erro ao converter o resultado da operação")
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

    [NonAction]
    protected T Ok<T>(T data)
    {
        Response.StatusCode = 200;

        return data;
    }

    private T Created<T>(T data)
    {
        Response.StatusCode = 201;

        return data;
    }

    [NonAction]
    private MessageResult Ok(string message)
    {
        Response.StatusCode = 200;

        return new MessageResult(message);
    }

    [NonAction]
    protected T NotFound<T>(T data)
    {
        Response.StatusCode = 404;

        return data;
    }

    [NonAction]
    private MessageResult NotFound(string message)
    {
        Response.StatusCode = 404;

        return new MessageResult(message);
    }

    [NonAction]
    public T Unauthorized<T>(T data)
    {
        Response.StatusCode = 401;

        return data;
    }

    [NonAction]
    private MessageResult Unauthorized(string message)
    {
        Response.StatusCode = 401;

        return new MessageResult(message);
    }

    [NonAction]
    public T BadRequest<T>(T data)
    {
        Response.StatusCode = 400;

        return data;
    }

    [NonAction]
    private MessageResult BadRequest(string message)
    {
        Response.StatusCode = 400;

        return new MessageResult(message);
    }

    [NonAction]
    private MessageResult InternalServerError(string message)
    {
        Response.StatusCode = 500;

        return new MessageResult(message);
    }
}