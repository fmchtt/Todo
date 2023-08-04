using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Application.Commands.CommentCommands;
using Todo.Application.Queries;
using Todo.Application.Results;
using Todo.Domain.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("comments")]
public class CommentController : TodoBaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CommentController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{itemId:guid}"), Authorize]
    public async Task<PaginatedResult<CommentResult>> GetByItemId([FromRoute] Guid itemId, [FromQuery] int page = 1)
    {
        var query = new GetAllCommentsQuery(GetUser(), itemId, page);
        var result = await _mediator.Send(query);

        var comments = result.Results.Select(comment => _mapper.Map<CommentResult>(comment)).ToList();

        return new PaginatedResult<CommentResult>(comments, result.PageCount);
    }

    [HttpPost("{itemId:guid}"), Authorize]
    public async Task<CommentResult> CreateComment(CreateCommentCommand command, [FromRoute] Guid itemId)
    {
        var user = GetUser();
        command.User = user;
        command.ItemId = itemId;

        var result = await _mediator.Send(command);

        return _mapper.Map<CommentResult>(result);
    }
    
    [HttpPatch("{commentId:guid}"), Authorize]
    public async Task<CommentResult> EditComment(EditCommentCommand command, [FromRoute] Guid commentId)
    {
        command.User = GetUser();
        command.CommentId = commentId;

        var result = await _mediator.Send(command);

        return _mapper.Map<CommentResult>(result);
    }
    
    [HttpDelete("{commentId:guid}"), Authorize]
    public async Task<MessageResult> CreateComment([FromRoute] Guid commentId)
    {
        var command = new DeleteCommentCommand(GetUser(), commentId);

        var result = await _mediator.Send(command);

        return new MessageResult(result);
    }
}