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
        [FromServices] ITodoItemRepostory todoItemRepository,
        [FromQuery] int page = 1
    )
    {
        if (page < 1)
        {
            page = 1;
        }

        var todos = todoItemRepository.GetAll(GetUserId(), page - 1);

        var result = todos.Results.Select(item => new ExpandedItemResult(item)).ToList();

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
        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpPost("{itemId:guid}/column/{columnId:guid}"), Authorize]
    [ProducesResponseType(typeof(ItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic ChangeColumn(
        Guid itemId,
        Guid columnId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        var command = new ChangeItemColumnCommand(itemId, columnId);
        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpPatch("{itemId:guid}"), Authorize]
    [ProducesResponseType(typeof(ItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic UpdateItem(
        EditItemCommand command,
        Guid itemId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        command.ItemId = itemId;
        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpDelete("{itemId:guid}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public dynamic DeleteItem(
        Guid itemId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        var command = new DeleteItemCommand(itemId);
        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpPost("{itemId:guid}/done"), Authorize]
    [ProducesResponseType(typeof(ItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic MarkAsDone(
        [FromRoute] Guid itemId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        var result = handler.Handle(new MarkCommand(itemId, true), user);

        return ParseResult(result);
    }

    [HttpPost("{itemId:guid}/undone"), Authorize]
    [ProducesResponseType(typeof(ItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic MarkAsUndone(
        [FromRoute] Guid itemId,
        [FromServices] ItemHandler handler
    )
    {
        var user = GetUser();
        var result = handler.Handle(new MarkCommand(itemId, true), user);

        return ParseResult(result);
    }
}
