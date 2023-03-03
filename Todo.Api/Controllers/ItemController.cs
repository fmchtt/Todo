using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Domain.Commands.ItemCommands;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("itens")]
public class ItemController : TodoBaseController
{
    [HttpGet, Authorize]
    public PaginatedDTO<ExpandedItemResult> GetAll(
        [FromServices] ITodoItemRepostory todoItemRepostory,
        [FromQuery] int page = 1
    )
    {
        if (page < 1)
        {
            page = 1;
        }

        var todos = todoItemRepostory.GetAll(GetUserId(), page - 1);

        var result = new List<ExpandedItemResult>();
        foreach (var item in todos.Results)
        {
            result.Add(new ExpandedItemResult(item));
        }

        return new PaginatedDTO<ExpandedItemResult>(result, todos.PageCount);
    }

    [HttpPost, Authorize]
    [ProducesResponseType(typeof(ItemResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public dynamic CreateItem(
        CreateItemCommand command, 
        [FromServices] ItemHandler handler
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

    [HttpPost("{itemId}/column/{columnId}"), Authorize]
    [ProducesResponseType(typeof(ItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic ChangeColumn(
        ChangeItemColumnCommand command,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        if (user == null )
        {
            return NotFound();
        }

        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpPatch("{itemId}"), Authorize]
    [ProducesResponseType(typeof(ItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic UpdateItem(
        EditItemCommand command,
        [FromServices] ItemHandler handler
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

    [HttpDelete("{itemId}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public dynamic DeleteItem(
        DeleteItemCommand command,
        [FromServices] ItemHandler handler
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

    [HttpPost("{itemId}/done"), Authorize]
    [ProducesResponseType(typeof(ItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic MarkAsDone(
        [FromRoute] Guid itemId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = handler.Handle(new MarkCommand(itemId, true), user);

        return ParseResult(result);
    }

    [HttpPost("{itemId}/undone"), Authorize]
    [ProducesResponseType(typeof(ItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic MarkAsunDone(
        [FromRoute] Guid itemId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = handler.Handle(new MarkCommand(itemId, true), user);

        return ParseResult(result);
    }
}
