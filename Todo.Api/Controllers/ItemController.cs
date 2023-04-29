using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Domain.Commands.ItemCommands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("itens")]
public class ItemController : TodoBaseController
{
    [HttpGet(""), Authorize]
    public PaginatedResult<ExpandedItemResult> GetAll(
        [FromServices] ITodoItemRepository todoItemRepository,
        [FromServices] IMapper mapper,
        [FromQuery] int page = 1
    )
    {
        if (page < 1)
        {
            page = 1;
        }

        var todos = todoItemRepository.GetAll(GetUserId(), page - 1);

        var result = todos.Results.Select(mapper.Map<ExpandedItemResult>).ToList();

        return new PaginatedResult<ExpandedItemResult>(result, todos.PageCount);
    }

    [HttpPost, Authorize]
    [ProducesResponseType(typeof(ResumedItemResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public IActionResult CreateItem(
        CreateItemCommand command,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        var result = handler.Handle(command, user);

        return ParseResult<TodoItem, ResumedItemResult>(result);
    }

    [HttpPost("{itemId:guid}/column/{columnId:guid}"), Authorize]
    [ProducesResponseType(typeof(ResumedItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public IActionResult ChangeColumn(
        Guid itemId,
        Guid columnId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        var command = new ChangeItemColumnCommand
        {
            ItemId = itemId,
            ColumnId = columnId
        };
        var result = handler.Handle(command, user);

        return ParseResult<TodoItem, ResumedItemResult>(result);
    }

    [HttpPatch("{itemId:guid}"), Authorize]
    [ProducesResponseType(typeof(ResumedItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public IActionResult UpdateItem(
        EditItemCommand command,
        Guid itemId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        command.ItemId = itemId;
        var result = handler.Handle(command, user);

        return ParseResult<TodoItem, ResumedItemResult>(result);
    }

    [HttpDelete("{itemId:guid}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public IActionResult DeleteItem(
        Guid itemId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        var command = new DeleteItemCommand
        {
            ItemId = itemId
        };
        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpPost("{itemId:guid}/done"), Authorize]
    [ProducesResponseType(typeof(ResumedItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public IActionResult MarkAsDone(
        [FromRoute] Guid itemId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        var result = handler.Handle(new MarkCommand
        {
            ItemId = itemId,
            Done = true
        }, user);

        return ParseResult<TodoItem, ResumedItemResult>(result);
    }

    [HttpPost("{itemId:guid}/undone"), Authorize]
    [ProducesResponseType(typeof(ResumedItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public IActionResult MarkAsUndone(
        [FromRoute] Guid itemId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        var result = handler.Handle(new MarkCommand
        {
            ItemId = itemId,
            Done = false
        }, user);

        return ParseResult<TodoItem, ResumedItemResult>(result);
    }
}