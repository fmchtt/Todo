using Todo.Domain.Commands.UserCommands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;
using Todo.Domain.Utils;

namespace Todo.Domain.Handlers;

public class UserHandler : IHandlerPublic<LoginCommand, User>, IHandlerPublic<RegisterCommand, User>, IHandler<EditUserCommand, User>, IHandlerPublic<RecoverPasswordCommand>, IHandlerPublic<ConfirmRecoverPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IHasher _hasher;
    private readonly IRecoverCodeRepository _recoverCodeRepository;
    private readonly IMailer _mailer;

    public UserHandler(IUserRepository userRepository, IHasher hasher, IRecoverCodeRepository recoverCodeRepository, IMailer mailer)
    {
        _userRepository = userRepository;
        _hasher = hasher;
        _recoverCodeRepository = recoverCodeRepository;
        _mailer = mailer;
    }
    
    public CommandResult<User> Handle(LoginCommand command)
    {
        var user = _userRepository.GetByEmail(command.Email);
        if (user == null || !_hasher.Verify(command.Password, user.Password))
        {
            return new CommandResult<User>(Code.NotFound, "Usuário ou senha inválidos");
        }

        return new CommandResult<User>(Code.Ok, "Logado com sucesso!", user);
    }

    public CommandResult<User> Handle(RegisterCommand command)
    {
        var existingUser = _userRepository.GetByEmail(command.Email);
        if (existingUser != null)
        {
            return new CommandResult<User>(Code.Invalid, "Usuário com o email já existe");
        }

        var password = _hasher.Hash(command.Password);
        var user = new User(command.Name, command.Email, password, null);
        _userRepository.Create(user);

        return new CommandResult<User>(Code.Created, "Usuário criado com sucesso!", user);
    }

    public CommandResult<User> Handle(EditUserCommand command, User user)
    {
        if (command.Name != null && command.Name != user.Name)
        {
            user.Name = command.Name;
        }

        if (command.AvatarUrl != null && command.AvatarUrl != user.AvatarUrl)
        {
            user.AvatarUrl = command.AvatarUrl;
        }

        _userRepository.Update(user);

        return new CommandResult<User>(Code.Ok, "Usuario editado com sucesso!", user);
    }

    public CommandResult HandleDelete(User user)
    {
        _userRepository.Delete(user);

        return new CommandResult(Code.Ok, "Usuario deletado com sucesso!");
    }

    public CommandResult Handle(RecoverPasswordCommand command)
    {
        var user = _userRepository.GetByEmail(command.Email);
        if (user == null)
        {
            return new CommandResult(Code.NotFound, "Usuário não encontrado!");
        }

        int code;
        var existingRecoverCode = _recoverCodeRepository.Get(command.Email);
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

        _mailer.SendMail(user.Email, $"Seu código de recuperação de senha: {code}.").Wait();

        return new CommandResult(Code.Ok, "Enviado com sucesso!");
    }

    public CommandResult Handle(ConfirmRecoverPasswordCommand command)
    {
        var code = _recoverCodeRepository.Get(command.Code, command.Email);
        if (code == null)
        {
            return new CommandResult(Code.NotFound, "Usuário não encontrado!");
        }

        code.User.Password = _hasher.Hash(command.Password);
        _userRepository.Update(code.User);

        _recoverCodeRepository.Delete(code);

        return new CommandResult(Code.Ok, "Senha alterada com sucesso!");
    }
}