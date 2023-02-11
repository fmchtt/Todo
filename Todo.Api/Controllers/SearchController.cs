using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.Repositories;

namespace Todo.Api.Controllers;

[ApiController, Route("search")]
public class SearchController : TodoBaseController
{
    [HttpGet, Authorize]
    public SearchResultDTO Search([FromQuery] string s, [FromServices] ITodoItemRepostory itemRepostory, [FromServices] IBoardRepository boardRepository)
    {
        if (s == null || s == String.Empty)
        {
            return new SearchResultDTO();
        }

        var boards = boardRepository.GetAllByName(s);
        var itens = itemRepostory.GetAllByTitle(s);

        var result = new SearchResultDTO();
        foreach (var board in boards)
        {
            result.Boards.Add(new BoardSearchResultDTO(board.Id, board.Name));
        }
        foreach (var item in itens)
        {
            result.Itens.Add(new TodoItemSearchResultDTO(item.Id, item.Title));
        }

        return result;
    }
}
