using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Domain.Commands.BoardCommands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("boards")]
public class BoardController : TodoBaseController
{
    [HttpGet, Authorize]
    public PaginatedResult<ResumedBoardResult> GetAll(
        [FromServices] IBoardRepository boardRepository,
        [FromServices] IMapper mapper,
        [FromQuery] int page = 1
    )
    {
        if (page < 1)
        {
            page = 1;
        }

        var todos = boardRepository.GetAll(GetUserId(), page - 1);

        var result = todos.Results.Select(mapper.Map<ResumedBoardResult>).ToList();

        return new PaginatedResult<ResumedBoardResult>(result, todos.PageCount);
    }

    [HttpGet("{id:guid}"), Authorize]
    [ProducesResponseType(typeof(ExpandedBoardResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public IActionResult GetById(
        [FromRoute] Guid id,
        [FromServices] IBoardRepository boardRepository,
        [FromServices] IMapper mapper
    )
    {
        var board = boardRepository.GetById(id);
        var user = GetUser();
        if (board == null || !board.Participants.Contains(user))
        {
            return NotFound(new MessageResult("Quadro não encontrado!"));
        }

        return Ok(mapper.Map<ExpandedBoardResult>(board));
    }

    [HttpPost, Authorize]
    [ProducesResponseType(typeof(ResumedBoardResult), 201)]
    public IActionResult Create(
        CreateBoardCommand command,
        [FromServices] BoardHandler handler
    )
    {
        var user = GetUser();
        var result = handler.Handle(command, user);

        return ParseResult<Board, ResumedBoardResult>(result);
    }

    [HttpPatch("{boardId:guid}"), Authorize]
    [ProducesResponseType(typeof(ResumedBoardResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public IActionResult EditBoard(
        EditBoardCommand command,
        Guid boardId,
        [FromServices] BoardHandler handler
    )
    {
        var user = GetUser();
        command.BoardId = boardId;
        var result = handler.Handle(command, user);

        return ParseResult<Board, ResumedBoardResult>(result);
    }


    [HttpDelete("{boardId:guid}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public IActionResult DeleteBoard(
        Guid boardId,
        [FromServices] BoardHandler handler
    )
    {
        var user = GetUser();
        var command = new DeleteBoardCommand
        {
            BoardId = boardId
        };
        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpGet("{boardId:guid}/invite/confirm")]
    [ProducesResponseType(typeof(MessageResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public IActionResult ConfirmInvite(
        Guid boardId,
        [FromServices] BoardHandler handler
    )
    {
        var user = GetUser();
        var command = new ConfirmBoardParticipantCommand
        {
            BoardId = boardId
        };
        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpPost("{boardId:guid}/invite"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public IActionResult InviteParticipant(
        AddBoardParticipantCommand command,
        Guid boardId,
        [FromServices] BoardHandler handler
    )
    {
        var user = GetUser();
        command.BoardId = boardId;
        command.Domain = HttpContext.Request.Host.ToString();
        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpDelete("{boardId:guid}/participant/{participantId:guid}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public IActionResult RemoveParticipant(
        Guid boardId,
        Guid participantId,
        [FromServices] BoardHandler handler
    )
    {
        var user = GetUser();
        var command = new RemoveBoardParticipantCommand
        {
            BoardId = boardId,
            ParticipantId = participantId
        };
        var result = handler.Handle(command, user);

        return ParseResult(result);
    }
}