using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Repositories;
using Todo.Domain.UseCases.ItemUseCases;

namespace Todo.Api.Controllers;

[ApiController, Route("itens")]
public class ItemController : TodoBaseController
{
    [HttpGet, Authorize]
    public PaginatedDTO<ExpandedTodoItemResultDTO> GetAll(
        [FromServices] ITodoItemRepostory todoItemRepostory,
        [FromQuery] int page = 1
    )
    {
        if (page < 1)
        {
            page = 1;
        }

        var todos = todoItemRepostory.GetAll(GetUserId(), page - 1);

        var result = new List<ExpandedTodoItemResultDTO>();
        foreach (var item in todos.Results)
        {
            result.Add(new ExpandedTodoItemResultDTO(item));
        }

        return new PaginatedDTO<ExpandedTodoItemResultDTO>(result, todos.PageCount);
    }

    [HttpPost, Authorize]
    [ProducesResponseType(typeof(TodoItemResultDTO), 201)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public dynamic CreateItem(
        [FromBody] CreateTodoItemDTO data, 
        [FromServices] ITodoItemRepostory itemRepostory, 
        [FromServices] IBoardRepository boardRepository, 
        [FromServices] IColumnRepository columnRepository
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = new CreateTodoItemUseCase(itemRepostory, boardRepository, columnRepository).Handle(data, user);

        return ParseResult(result);
    }

    [HttpPost("{todoId}/column/{columnId}"), Authorize]
    [ProducesResponseType(typeof(TodoItemResultDTO), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic ChangeColumn(
        [FromRoute] string todoId, 
        [FromRoute] string columnId, 
        [FromServices] ITodoItemRepostory itemRepostory, 
        [FromServices] IColumnRepository columnRepository
    )
    {
        var user = GetUser();
        if (user == null )
        {
            return NotFound();
        }

        var result = new ChangeItemColumnUseCase(itemRepostory, columnRepository).Handle(Guid.Parse(columnId), Guid.Parse(todoId), user);

        return ParseResult(result);
    }

    [HttpPatch("{id}"), Authorize]
    [ProducesResponseType(typeof(TodoItemResultDTO), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic UpdateItem(
        [FromBody] EditTodoItemDTO data, 
        [FromRoute] string id, 
        [FromServices] ITodoItemRepostory itemRepostory
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = new EditTodoItemUseCase(itemRepostory).Handle(data, Guid.Parse(id), user);

        return ParseResult(result);
    }

    [HttpDelete("{id}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public dynamic DeleteItem(
        [FromRoute] string id, 
        [FromServices] ITodoItemRepostory itemRepostory
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }
        var result = new DeleteTodoItemUseCase(itemRepostory).Handle(Guid.Parse(id), user);

        return ParseResult(result);
    }

    [HttpPost("{id}/done"), Authorize]
    [ProducesResponseType(typeof(TodoItemResultDTO), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic MarkAsDone(
        [FromRoute] string id,
        [FromServices] ITodoItemRepostory todoItemRepostory
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = new MarkAsDoneUseCase(todoItemRepostory).Handle(Guid.Parse(id), user);

        return ParseResult(result);
    }

    [HttpPost("{id}/undone"), Authorize]
    [ProducesResponseType(typeof(TodoItemResultDTO), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic MarkAsunDone(
        [FromRoute] string id,
        [FromServices] ITodoItemRepostory todoItemRepostory
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = new MarkAsUndoneUseCase(todoItemRepostory).Handle(Guid.Parse(id), user);

        return ParseResult(result);
    }
}
