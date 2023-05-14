using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Application.Commands.UserCommands;
using Todo.Application.DTO;
using Todo.Application.Handlers;
using Todo.Application.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("users")]
public class UserController : TodoBaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPatch, Authorize]
    [ProducesResponseType(typeof(ResumedUserResult), 200)]
    public async Task<ResumedUserResult> ChangeAvatar(
        [FromForm] EditUserApiDTO data
    )
    {
        var user = GetUser();
        var command = new EditUserCommand(
            data.Name,
            data.File != null
                ? new UploadedFile(data.File.OpenReadStream(), data.File.Name, data.File.ContentType)
                : null,
            user
        );
        var result = await _mediator.Send(command);

        return _mapper.Map<ResumedUserResult>(result);
    }

    [HttpDelete, Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    public async Task<MessageResult> DeleteUser([FromServices] UserHandler handler)
    {
        var user = GetUser();
        var command = new DeleteUserCommand(user);
        var result = await _mediator.Send(command);

        return new MessageResult(result);
    }
}