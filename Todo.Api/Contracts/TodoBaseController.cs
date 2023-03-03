using Microsoft.AspNetCore.Mvc;
using Todo.Api.DTO;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Api.Contracts;

public class TodoBaseController : ControllerBase
{
    [NonAction]
    public dynamic ParseResult<T>(CommandResult<T> result)
    {
        var message = result.Message;

        if (result.Code == Code.Ok)
        {
            if (result.Result != null)
            {
                #pragma warning disable CS8603
                return Ok(result.Result);
            }

            return Ok(message);
        }

        if (result.Code == Code.Created)
        {
            if (result.Result != null)
            {
                #pragma warning disable CS8603
                return Created(result.Result);
            }

            return Created(message);
        }

        if (result.Code == Code.Invalid)
        {
            if (result.Errors != null)
            {
                return BadRequest(result.Errors);
            }

            return BadRequest(message);
        }

        if (result.Code == Code.NotFound)
        {
            return NotFound(message);
        }

        if (result.Code == Code.Unauthorized)
        {
            return Unauthorized(message);
        }

        return InternalServerError("Erro ao converter o resultado da operação");
    }

    [NonAction]
    public dynamic ParseResult(CommandResult result)
    {
        var message = result.Message;

        if (result.Code == Code.Ok)
        {
            return Ok(message);
        }

        if (result.Code == Code.Created)
        {
            return Created(message);
        }

        if (result.Code == Code.Invalid)
        {
            if (result.Errors != null)
            {
                return BadRequest(result.Errors);
            }

            return BadRequest(message);
        }

        if (result.Code == Code.NotFound)
        {
            return NotFound(message);
        }

        if (result.Code == Code.Unauthorized)
        {
            return Unauthorized(message);
        }

        return InternalServerError("Erro ao converter o resultado da operação");
    }

    [NonAction]
    public Guid GetUserId()
    {
        var userId = User?.Identity?.Name;
        if (userId == null)
        {
            return Guid.Empty;
        }

        return Guid.Parse(userId);
    }

    [NonAction]
    public User? GetUser()
    {
        var _userRepository = HttpContext.RequestServices.GetService<IUserRepository>();
        if (_userRepository == null)
        {
            return null;
        }

        var userId = GetUserId();
        var user = _userRepository.GetById(userId);

        return user;
    }

    [NonAction]
    public T Ok<T>(T data)
    {
        Response.StatusCode = 200;

        return data;
    }

    public T Created<T>(T data)
    {
        Response.StatusCode = 201;

        return data;
    }

    [NonAction]
    public MessageResult Ok(string message)
    {
        Response.StatusCode = 200;

        return new MessageResult(message);
    }

    [NonAction]
    public T NotFound<T>(T data)
    {
        Response.StatusCode = 404;

        return data;
    }

    [NonAction]
    public MessageResult NotFound(string message)
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
    public MessageResult Unauthorized(string message)
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
    public MessageResult BadRequest(string message)
    {
        Response.StatusCode = 400;

        return new MessageResult(message);
    }

    [NonAction]
    public MessageResult InternalServerError(string message)
    {
        Response.StatusCode = 500;

        return new MessageResult(message);
    }
}
