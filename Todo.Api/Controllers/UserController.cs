using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.Commands.UserCommands;
using Todo.Domain.Handlers;
using Todo.Domain.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("users")]
public class UserController : TodoBaseController
{
    [HttpPatch, Authorize]
    [ProducesResponseType(typeof(ResumedUserResult), 200)]
    public async Task<dynamic> ChangeAvatar(
        [FromForm] EditUserApiDTO data, 
        [FromServices] IFileStorage fileStorage,
        [FromServices] UserHandler handler
    )
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        string? avatarUrl = null;
        if (data.File != null)
        {
            avatarUrl = await fileStorage.SaveFile(data.File);
        }

        var command = new EditUserCommand(data.Name, avatarUrl);
        var result = handler.Handle(command, user);

        return ParseResult(result);
    }

    [HttpDelete, Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    public dynamic DeleteUser([FromServices] UserHandler handler)
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = handler.HandleDelete(user);

        return ParseResult(result);
    }
}
