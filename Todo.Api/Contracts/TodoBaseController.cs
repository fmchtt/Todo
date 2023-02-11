using Microsoft.AspNetCore.Mvc;
using Todo.Api.DTO;
using Todo.Domain.DTO;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Api.Contracts;

public class TodoBaseController : ControllerBase
{
    [NonAction]
    public dynamic ParseResult<T>(ResultDTO<T> result)
    {
        var message = result.Message;

        if (result.Code == 200)
        {
            if (result.Result != null)
            {
                return Ok(result.Result);
            }

            return Ok(message);
        }

        if (result.Code == 400)
        {
            if (result.Errors != null)
            {
                return BadRequest(result.Errors);
            }

            return BadRequest(message);
        }

        if (result.Code == 404)
        {
            return NotFound(message);
        }

        if (result.Code == 401)
        {
            return Unauthorized(message);
        }

        return InternalServerError("Erro ao converter o resultado da operação");
    }

    [NonAction]
    public dynamic ParseResult(ResultDTO result)
    {
        var message = result.Message;

        if (result.Code == 200)
        {
            return Ok(message);
        }

        if (result.Code == 400)
        {
            if (result.Errors != null)
            {
                return BadRequest(result.Errors);
            }

            return BadRequest(message);
        }

        if (result.Code == 404)
        {
            return NotFound(message);
        }

        if (result.Code == 401)
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
    public User? GetUser(IUserRepository repository)
    {
        var userId = GetUserId();
        var user = repository.GetById(userId);

        return user;
    }

    [NonAction]
    public T Ok<T>(T data)
    {
        Response.StatusCode = 200;

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
