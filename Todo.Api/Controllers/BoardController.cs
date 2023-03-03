using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Domain.Commands.BoardCommands;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("boards")]
public class BoardController : TodoBaseController
{
    [HttpGet, Authorize]
    public PaginatedDTO<ResumedBoardResult> GetAll(
        [FromServices] IBoardRepository boardRepository,
        [FromQuery] int page = 1
    )
    {
        if (page < 1)
        {
            page = 1;
        }

        var todos = boardRepository.GetAll(GetUserId(), page - 1);

        var result = new List<ResumedBoardResult>();
        foreach (var board in todos.Results)
        {
            result.Add(new ResumedBoardResult(board));
        }

        return new PaginatedDTO<ResumedBoardResult>(result, todos.PageCount);
    }

    [HttpGet("{id}"), Authorize]
    [ProducesResponseType(typeof(ExpandedBoardResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic GetById(
        [FromRoute] string id,
        [FromServices] IBoardRepository boardRepository
    )
    {
        var boardId = Guid.Parse(id);
        var board = boardRepository.GetById(boardId);
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        if (board == null || !board.Participants.Contains(user))
        {
            return NotFound(new MessageResult("Quadro não encontrado!"));
        }

        return new ExpandedBoardResult(board);
    }

    [HttpPost, Authorize]
    [ProducesResponseType(typeof(ResumedBoardResult), 201)]
    public dynamic Create(
        CreateBoardCommand command,
        [FromServices] BoardHandler handler
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

    [HttpPatch("{boardId}"), Authorize]
    [ProducesResponseType(typeof(ResumedBoardResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic EditBoard(
        EditBoardCommand command,
        [FromServices] BoardHandler handler
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


    [HttpDelete("{boardId}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public dynamic DeleteBoard(
        DeleteBoardCommand command,
        [FromServices] BoardHandler handler
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

    [HttpGet("{boardId}/invite/confirm")]
    [ProducesResponseType(typeof(MessageResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic ConfirmInvite(
        ConfirmBoardParticipantCommand command,
        [FromServices] BoardHandler handler
    )
    {
        var user = GetUser();
        if ( user == null )
        {
            return NotFound();
        }

        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpPost("{boardId}/invite"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 201)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic InviteParticipant(
        AddBoardParticipantCommand command,
        [FromServices] BoardHandler handler
    )
    {
        var user = GetUser();
        if (user == null )
        {
            return NotFound();
        }

        command.Domain = HttpContext.Request.Host.ToString();

        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpDelete("{boardId}/participant/{participantId}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic RemoveParticipant(
       RemoveBoardParticipantCommand command,
       [FromServices] BoardHandler handler
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
}
