using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.Commands.UserCommands;
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
        [FromServices] IFileStorage fileStorage,
        [FromServices] UserHandler handler
    )
    {
        var user = GetUser();
        string? avatarUrl = null;
        if (data.File != null)
        {
            avatarUrl = await fileStorage.SaveFile(data.File);
        }

        var command = new EditUserCommand
        {
            Name = data.Name,
            AvatarUrl = avatarUrl
        };
        var result = handler.Handle(command, user);

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