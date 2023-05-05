using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.Commands.UserCommands;
using Todo.Domain.DTO;
using Todo.Domain.Handlers;
using Todo.Domain.Results;
using Todo.Domain.Entities;

namespace Todo.Api.Controllers;

[ApiController, Route("users")]
public class UserController : TodoBaseController
{
    [HttpPatch, Authorize]
    [ProducesResponseType(typeof(ResumedUserResult), 200)]
    public async Task<IActionResult> ChangeAvatar(
        [FromForm] EditUserApiDTO data,
        [FromServices] UserHandler handler
    )
    {
        var command = new EditUserCommand
        {
            Name = data.Name,
            Avatar = data.File != null ? new FileDTO(data.File.OpenReadStream(), data.File.FileName, data.File.ContentType) : null,
            User = GetUser()
        };
        var result = await handler.Handle(command);

        return ParseResult<User, ResumedUserResult>(result);
    }

    [HttpDelete, Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    public IActionResult DeleteUser([FromServices] UserHandler handler)
    {
        var user = GetUser();
        var result = handler.HandleDelete(user);

        return ParseResult(result);
    }
}