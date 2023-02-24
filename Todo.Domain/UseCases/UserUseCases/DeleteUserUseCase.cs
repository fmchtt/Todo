using Todo.Domain.DTO;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases.UserUseCases;

public class DeleteUserUseCase
{
    private readonly IUserRepository _userRepository;

    public DeleteUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ResultDTO Handle(User user)
    {
        _userRepository.Delete(user);

        return new ResultDTO(200, "Usuario deletado com sucesso!");
    }
}
