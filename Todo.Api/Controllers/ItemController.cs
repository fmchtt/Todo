using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Repositories;
using Todo.Domain.UseCases;

namespace Todo.Api.Controllers;

[ApiController, Route("itens")]
public class ItemController : TodoBaseController
{
    [HttpGet, Authorize]
    public List<TodoItemResultDTO> GetAll(
        [FromServices] ITodoItemRepostory todoItemRepostory
    )
    {
        var todos = todoItemRepostory.GetAll(GetUserId());

        var result = new List<TodoItemResultDTO>();
        foreach (var item in todos)
        {
            result.Add(new TodoItemResultDTO(item));
        }

        return result;
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
}
