using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Domain.Utils;

namespace Todo.Domain.UseCases;

public class LoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IHasher _hasher;

    public LoginUseCase(IUserRepository userRepository, IHasher hasher) {
        _userRepository = userRepository;
        _hasher = hasher;
    }
    
    public ResultDTO<User> Handle(LoginDTO data)
    {
        var user = _userRepository.GetByEmail(data.Email);
        if (user == null || !_hasher.Verify(data.Password, user.Password))
        {
            return new ResultDTO<User>(400, "Usuário ou senha inválidos");
        }

        return new ResultDTO<User>(200, "Logado com sucesso!", user);
    }
}
