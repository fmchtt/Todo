using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Domain.Commands.ColumnCommands;
using Todo.Domain.Handlers;
using Todo.Domain.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("columns")]
public class ColumnController : TodoBaseController
{
    [HttpPost, Authorize]
    [ProducesResponseType(typeof(ResumedColumnResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 400)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public dynamic Create(
        CreateColumnCommand command, 
        [FromServices] ColumnHandler handler
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpPatch("{columnId}"), Authorize]
    [ProducesResponseType(typeof(ResumedColumnResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic EditColumn(
        EditColumnCommand command,
        [FromServices] ColumnHandler handler
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpDelete("{columnId}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public dynamic DeleteColumn(
        DeleteColumnCommand command, 
        [FromServices] ColumnHandler handler
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = handler.Handle(command, user);

        return ParseResult(result);
    }
}
