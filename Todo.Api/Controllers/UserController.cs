using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Repositories;
using Todo.Domain.UseCases;

namespace Todo.Api.Controllers;

[ApiController, Route("users")]
public class UserController : TodoBaseController
{
    [HttpPatch, Authorize]
    [ProducesResponseType(typeof(UserResumedResultDTO), 200)]
    public async Task<dynamic> ChangeAvatar(
        [FromForm] EditUserApiDTO data, 
        [FromServices] IFileStorage fileStorage,
        [FromServices] IUserRepository userRepository
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

        var useCaseData = new EditUserDTO(data.Name, avatarUrl);
        var result = new EditUserUseCase(userRepository).Handle(useCaseData, user);

        return ParseResult(result);
    }

    [HttpDelete, Authorize]
    [ProducesResponseType(typeof(MessageResult), 200)]
    public dynamic DeleteUser([FromServices] IUserRepository userRepository)
    {
        var user = GetUser();
        if (user == null)
        {
            return NotFound();
        }

        var result = new DeleteUserUseCase(userRepository).Handle(user);

        return ParseResult(result);
    }
}
