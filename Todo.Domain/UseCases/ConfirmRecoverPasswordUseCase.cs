using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.Repositories;
using Todo.Domain.Utils;

namespace Todo.Domain.UseCases;

public class ConfirmRecoverPasswordUseCase
{
    private readonly IRecoverCodeRepository _recoverCodeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHasher _hasher;

    public ConfirmRecoverPasswordUseCase(IRecoverCodeRepository recoverCodeRepository, IHasher hasher, IUserRepository userRepository)
    {
        _recoverCodeRepository = recoverCodeRepository;
        _hasher = hasher;
        _userRepository = userRepository;
    }

    public ResultDTO Handle(ConfirmRecoverPasswordDTO data)
    {
        var code = _recoverCodeRepository.Get(data.Code, data.Email);
        if (code == null)
        {
            return new ResultDTO(404, "Usuário não encontrado!");
        }

        code.User.Password = _hasher.Hash(data.Password);
        _userRepository.Update(code.User);

        _recoverCodeRepository.Delete(code);

        return new ResultDTO(200, "Senha alterada com sucesso!");
    }
}
