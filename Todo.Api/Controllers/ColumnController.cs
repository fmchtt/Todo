using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Application.Commands.ColumnCommands;
using Todo.Application.Handlers;
using Todo.Application.Results;
using Todo.Domain.Entities;

namespace Todo.Api.Controllers;

[ApiController, Route("columns")]
public class ColumnController : TodoBaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ColumnController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost(""), Authorize]
    [ProducesResponseType(typeof(ResumedColumnResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 400)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public async Task<ResumedColumnResult> Create(
        CreateColumnCommand command
    )
    {
        command.User = GetUser();

        var result = await _mediator.Send(command);

        return _mapper.Map<Column, ResumedColumnResult>(result);
    }

    [HttpPatch("{columnId:guid}"), Authorize]
    [ProducesResponseType(typeof(ResumedColumnResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<ResumedColumnResult> EditColumn(
        EditColumnCommand command,
        Guid columnId
    )
    {
        command.ColumnId = columnId;
        command.User = GetUser();
        var result = await _mediator.Send(command);

        return _mapper.Map<Column, ResumedColumnResult>(result);
    }

    [HttpDelete("{columnId:guid}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public async Task<MessageResult> DeleteColumn(
        Guid columnId
    )
    {
        var command = new DeleteColumnCommand(columnId, GetUser());
        
        var result = await _mediator.Send(command);

        return new MessageResult(result);
    }
}