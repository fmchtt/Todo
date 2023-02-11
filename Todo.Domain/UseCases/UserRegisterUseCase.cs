using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Domain.Utils;

namespace Todo.Domain.UseCases;

public class UserRegisterUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IHasher _hasher;

    public UserRegisterUseCase(IUserRepository userRepository, IHasher hasher) {
        _userRepository = userRepository;
        _hasher = hasher;
    }

    public ResultDTO<User> Handle(UserCreateDTO data)
    {
        var existingUser = _userRepository.GetByEmail(data.Email);
        if (existingUser != null) {
            return new ResultDTO<User>(400, "Usuário com o email já existe");
        }

        var password = _hasher.Hash(data.Password);
        var user = new User(data.Name, data.Email, password);
        _userRepository.Create(user);

        return new ResultDTO<User>(200, "Usuário criado com sucesso!", user);
    }
}
