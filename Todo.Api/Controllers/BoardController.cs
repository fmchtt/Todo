using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Repositories;
using Todo.Domain.UseCases;

namespace Todo.Api.Controllers;

[ApiController, Route("boards")]
public class BoardController : TodoBaseController
{
    [HttpGet, Authorize]
    public List<BoardResultDTO> GetAll(
        [FromServices] IBoardRepository boardRepository
    )
    {
        var todos = boardRepository.GetAll(GetUserId());

        var result = new List<BoardResultDTO>();
        foreach (var board in todos)
        {
            result.Add(new BoardResultDTO(board));
        }

        return result;
    }

    [HttpGet("{id}"), Authorize]
    [ProducesResponseType(typeof(ExpandedBoardDTO), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic GetById(
        [FromRoute] string id,
        [FromServices] IBoardRepository boardRepository
    )
    {
        var boardId = Guid.Parse(id);
        var board = boardRepository.GetById(boardId);

        if (board == null || board.OwnerId != GetUserId())
        {
            return NotFound(new MessageResult("Quadro não encontrado!"));
        }

        return new ExpandedBoardDTO(board);
    }

    [HttpPost, Authorize]
    [ProducesResponseType(typeof(BoardResultDTO), 201)]
    public dynamic Create(
        [FromBody] CreateBoardDTO data,
        [FromServices] IBoardRepository boardRepository,
        [FromServices] IColumnRepository columnRepository
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }
        var result = new CreateBoardUseCase(boardRepository, columnRepository).Handle(data, user);

        return ParseResult(result);
    }

    [HttpPatch("{id}"), Authorize]
    [ProducesResponseType(typeof(BoardResultDTO), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic EditBoard(
        [FromRoute] string id,
        [FromBody] EditBoardDTO data,
        [FromServices] IBoardRepository boardRepository
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = new EditBoardUseCase(boardRepository).Handle(data, Guid.Parse(id), user);

        return ParseResult(result);
    }


    [HttpDelete("{id}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    public dynamic DeleteBoard(
        [FromRoute] string id,
        [FromServices] IBoardRepository boardRepository
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = new DeleteBoardUseCase(boardRepository).Handle(Guid.Parse(id), user);

        return ParseResult(result);
    }

    [HttpDelete("{boardId}/participant/{participantId}"), Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 401)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public dynamic RemoveParticipant(
       [FromRoute] string boardId,
       [FromRoute] string participantId,
       [FromServices] IBoardRepository boardRepository
    )
    {
        var user = GetUser();
        if (user == null )
        {
            return NotFound();
        }

        var result = new RemoveParticipantUseCase(boardRepository).Handle(Guid.Parse(boardId), Guid.Parse(participantId), user);

        return ParseResult(result);
    }
}
