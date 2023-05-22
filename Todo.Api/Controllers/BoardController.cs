using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Application.Commands.BoardCommands;
using Todo.Application.Queries;
using Todo.Application.Results;
using Todo.Domain.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("boards")]
public class BoardController : TodoBaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BoardController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet, Authorize]
    public async Task<PaginatedResult<ResumedBoardResult>> GetAll(
        [FromQuery] int page = 1
    )
    {
        if (page < 1)
        {
            page = 1;
        }

        var query = new GetAllBoardsQuery(page, GetUser());

        var boards = await _mediator.Send(query);

        var result = boards.Results.Select(_mapper.Map<ResumedBoardResult>).ToList();

        return new PaginatedResult<ResumedBoardResult>(result, boards.PageCount);
    }

    [HttpGet("{id:guid}"), Authorize]
    [ProducesResponseType(typeof(ExpandedBoardResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<ExpandedBoardResult> GetById(
        [FromRoute] Guid id
    )
    {
        var query = new GetBoardByIdQuery(id, GetUser());
        var board = await _mediator.Send(query);

        return _mapper.Map<ExpandedBoardResult>(board);
    }

    [HttpPost, Authorize]
    [ProducesResponseType(typeof(ResumedBoardResult), 201)]
    public async Task<ResumedBoardResult> Create(
        [FromBody] CreateBoardCommand command
    )
    {
        command.User = GetUser();
        var result = await _mediator.Send(command);

        return _mapper.Map<ResumedBoardResult>(result);
    }

    [HttpPatch("{boardId:guid}"), Authorize]
    [ProducesResponseType(typeof(ResumedBoardResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<ResumedBoardResult> EditBoard(
        [FromBody] EditBoardCommand command,
        Guid boardId
    )
    {
        command.BoardId = boardId;
        command.User = GetUser();
        var result = await _mediator.Send(command);

        return _mapper.Map<ResumedBoardResult>(result);
    }


    [HttpDelete("{boardId:guid}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public async Task<MessageResult> DeleteBoard(
        Guid boardId
    )
    {
        var command = new DeleteBoardCommand(boardId, GetUser());
        
        var result = await _mediator.Send(command);

        return new MessageResult(result);
    }

    [HttpGet("{boardId:guid}/invite/confirm")]
    [ProducesResponseType(typeof(MessageResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<MessageResult> ConfirmInvite(
        Guid boardId
    )
    {
        var command = new ConfirmBoardParticipantCommand(boardId, GetUser());

        var result = await _mediator.Send(command);

        return new MessageResult(result);
    }

    [HttpPost("{boardId:guid}/invite"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<MessageResult> InviteParticipant(
        [FromBody] AddBoardParticipantCommand command,
        Guid boardId
    )
    {
        command.BoardId = boardId;
        command.Domain = HttpContext.Request.Host.ToString();
        command.User = GetUser();
        var result = await _mediator.Send(command);

        return new MessageResult(result);
    }

    [HttpDelete("{boardId:guid}/participant/{participantId:guid}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<MessageResult> RemoveParticipant(
        Guid boardId,
        Guid participantId
    )
    {
        var command = new RemoveBoardParticipantCommand(boardId, participantId, GetUser());
        
        var result = await _mediator.Send(command);

        return new MessageResult(result);
    }
}