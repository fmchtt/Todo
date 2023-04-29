using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Domain.Commands.ColumnCommands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("columns")]
public class ColumnController : TodoBaseController
{
    [HttpPost(""), Authorize]
    [ProducesResponseType(typeof(ResumedColumnResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 400)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public IActionResult Create(
        CreateColumnCommand command,
        [FromServices] ColumnHandler handler
    )
    {
        var user = GetUser();
        var result = handler.Handle(command, user);

        return ParseResult<Column, ResumedColumnResult>(result);
    }

    [HttpPatch("{columnId:guid}"), Authorize]
    [ProducesResponseType(typeof(ResumedColumnResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public IActionResult EditColumn(
        EditColumnCommand command,
        Guid columnId,
        [FromServices] ColumnHandler handler
    )
    {
        var user = GetUser();
        command.ColumnId = columnId;
        var result = handler.Handle(command, user);

        return ParseResult<Column, ResumedColumnResult>(result);
    }

    [HttpDelete("{columnId:guid}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public IActionResult DeleteColumn(
        Guid columnId,
        [FromServices] ColumnHandler handler
    )
    {
        var user = GetUser();
        var command = new DeleteColumnCommand
        {
            ColumnId = columnId
        };
        var result = handler.Handle(command, user);

        return ParseResult(result);
    }
}