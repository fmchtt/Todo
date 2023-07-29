using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Application.Commands.ItemCommands;
using Todo.Application.Queries;
using Todo.Application.Results;
using Todo.Domain.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("itens")]
public class ItemController : TodoBaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ItemController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet(""), Authorize]
    public async Task<PaginatedResult<ExpandedItemResult>> GetAll(
        [FromQuery] Guid? boardId,
        [FromQuery] bool? done,
        [FromQuery] int page = 1
    )
    {
        var query = new GetAllTodoItemQuery(GetUser(), page, boardId, done);

        var todos = await _mediator.Send(query);

        var result = todos.Results.Select(_mapper.Map<ExpandedItemResult>).ToList();

        return new PaginatedResult<ExpandedItemResult>(result, todos.PageCount);
    }

    [HttpPost, Authorize]
    [ProducesResponseType(typeof(ResumedItemResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public async Task<ResumedItemResult> CreateItem(
        CreateItemCommand command
    )
    {
        command.User = GetUser();
        var result = await _mediator.Send(command);

        return _mapper.Map<ResumedItemResult>(result);
    }

    [HttpPost("{itemId:guid}/column/{columnId:guid}"), Authorize]
    [ProducesResponseType(typeof(ResumedItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<ResumedItemResult> ChangeColumn(
        Guid itemId,
        Guid columnId
    )
    {
        var command = new ChangeItemColumnCommand(columnId, itemId, GetUser());
        
        var result = await _mediator.Send(command);

        return _mapper.Map<ResumedItemResult>(result);
    }

    [HttpPatch("{itemId:guid}"), Authorize]
    [ProducesResponseType(typeof(ResumedItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<ResumedItemResult> UpdateItem(
        EditItemCommand command,
        Guid itemId
    )
    {
        command.ItemId = itemId;
        command.User = GetUser();
        var result = await _mediator.Send(command);

        return _mapper.Map<ResumedItemResult>(result);
    }

    [HttpDelete("{itemId:guid}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public async Task<MessageResult> DeleteItem(
        Guid itemId
    )
    {
        var command = new DeleteItemCommand(itemId, GetUser());
        
        var result = await _mediator.Send(command);

        return new MessageResult(result);
    }

    [HttpPost("{itemId:guid}/done"), Authorize]
    [ProducesResponseType(typeof(ResumedItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<ResumedItemResult> MarkAsDone(
        [FromRoute] Guid itemId
    )
    {
        var command = new MarkCommand(itemId, true, GetUser());
        
        var result = await _mediator.Send(command);

        return _mapper.Map<ResumedItemResult>(result);
    }

    [HttpPost("{itemId:guid}/undone"), Authorize]
    [ProducesResponseType(typeof(ResumedItemResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<ResumedItemResult> MarkAsUndone(
        [FromRoute] Guid itemId
    )
    {
        var command = new MarkCommand(itemId, false, GetUser());
        
        var result = await _mediator.Send(command);

        return _mapper.Map<ResumedItemResult>(result);
    }
}