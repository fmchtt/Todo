using Todo.Domain.Commands.UserCommands;
using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Domain.Utils;

namespace Todo.Domain.UseCases.UserUseCases;

public class RecoverPasswordUseCase
{
    private readonly IMailer _mailer;
    private readonly IUserRepository _userRepository;
    private readonly IRecoverCodeRepository _recoverCodeRepository;

    public RecoverPasswordUseCase(IMailer mailer, IUserRepository userRepository, IRecoverCodeRepository codeRepository)
    {
        _mailer = mailer;
        _userRepository = userRepository;
        _recoverCodeRepository = codeRepository;
    }

    public async Task<ResultDTO> Handle(RecoverPasswordDTO data)
    {
        var user = _userRepository.GetByEmail(data.Email);
        if (user == null)
        {
            return new ResultDTO(404, "Usuário não encontrado!");
        }

        int code;
        var existingRecoverCode = _recoverCodeRepository.Get(data.Email);
        if (existingRecoverCode == null)
        {
            code = new Random().Next(100000, 999999);

            var recoverCode = new RecoverCode(user.Id, code);
            _recoverCodeRepository.Create(recoverCode);
        }
        else
        {
            code = existingRecoverCode.Code;
        }

        bool mailResult;
        do
        {
            mailResult = await _mailer.SendMail(user.Email, $"Seu código de recuperação de senha: {code}.");
        } while (mailResult != true);

        return new ResultDTO(200, "Enviado com sucesso!");
    }
}
