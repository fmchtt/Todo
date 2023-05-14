using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Api.Contracts;

public class TodoBaseController : ControllerBase
{
    [NonAction]
    protected Guid GetUserId()
    {
        var userId = User.Identity?.Name;
        return userId == null ? Guid.Empty : Guid.Parse(userId);
    }

    [NonAction]
    protected User GetUser()
    {
        var userRepository = HttpContext.RequestServices.GetService<IUserRepository>();
        if (userRepository == null)
        {
            throw new UnauthorizedAccessException("Usuário não encontrado!");
        }

        var userId = GetUserId();
        var user = userRepository.GetById(userId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Usuário não encontrado!");
        }

        return user;
    }
}