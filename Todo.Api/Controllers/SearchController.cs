using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Application.Queries;
using Todo.Application.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("search")]
public class SearchController : TodoBaseController
{
    private readonly IMediator _mediator;
    
    public SearchController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet, Authorize]
    public async Task<SearchResult> Search(
        [FromQuery] string s
    )
    {
        if (string.IsNullOrEmpty(s))
        {
            return new SearchResult();
        }

        var query = new SearchQuery(s, GetUser());
        var result = await _mediator.Send(query);

        return result;
    }
}
