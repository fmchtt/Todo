using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class EditUserUseCase
{
    private readonly IUserRepository _userRepository;

    public EditUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ResultDTO<UserResumedResultDTO> Handle(EditUserDTO data, User user)
    {
        if (data.Name != null && data.Name != user.Name)
        {
            user.Name = data.Name;
        }

        if (data.AvatarUrl != null && data.AvatarUrl != user.AvatarUrl)
        {
            user.AvatarUrl = data.AvatarUrl;
        }

        _userRepository.Update(user);

        return new ResultDTO<UserResumedResultDTO>(200, "Usuario editado com sucesso!", new UserResumedResultDTO(user));
    }
}
