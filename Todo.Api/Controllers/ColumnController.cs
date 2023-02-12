using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Repositories;
using Todo.Domain.UseCases;

namespace Todo.Api.Controllers;

[ApiController, Route("columns")]
public class ColumnController : TodoBaseController
{
    [HttpPost, Authorize]
    [ProducesResponseType(typeof(ColumnResultDTO), 201)]
    [ProducesResponseType(typeof(MessageResult), 400)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public dynamic Create(
        [FromBody] CreateColumnDTO data, 
        [FromServices] IColumnRepository columnRepository, 
        [FromServices] IBoardRepository boardRepository
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }
        var result = new CreateColumnUseCase(columnRepository, boardRepository).Handle(data, user);

        return ParseResult(result);
    }

    [HttpPatch("{id}"), Authorize]
    [ProducesResponseType(typeof(ColumnResultDTO), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic EditColumn(
        [FromRoute] string id,
        [FromBody] EditColumnDTO data,
        [FromServices] IColumnRepository columnRepository
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = new EditColumnUseCase(columnRepository).Handle(data, Guid.Parse(id), user);

        return ParseResult(result);
    }

    [HttpDelete("{id}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public dynamic DeleteColumn(
        [FromRoute] string id, 
        [FromServices] IColumnRepository columnRepository
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = new DeleteColumnUseCase(columnRepository).Handle(Guid.Parse(id), user);

        return ParseResult(result);
    }
}
