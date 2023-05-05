using Todo.Domain.Commands.UserCommands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;
using Todo.Domain.Results;
using Todo.Domain.Utils;

namespace Todo.Domain.Handlers;

public class UserHandler : IHandlerPublic<LoginCommand, TokenResult>, IHandlerPublic<RegisterCommand, TokenResult>,
    IHandlerAsync<EditUserCommand, User>, IHandlerPublic<RecoverPasswordCommand>,
    IHandlerPublic<ConfirmRecoverPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IHasher _hasher;
    private readonly IRecoverCodeRepository _recoverCodeRepository;
    private readonly IMailer _mailer;
    private readonly ITokenService _tokenService;
    private readonly IFileStorage _fileStorage;

    public UserHandler(
        IUserRepository userRepository,
        IHasher hasher,
        IRecoverCodeRepository recoverCodeRepository,
        IMailer mailer,
        ITokenService tokenService,
        IFileStorage fileStorage
    )
    {
        _userRepository = userRepository;
        _hasher = hasher;
        _recoverCodeRepository = recoverCodeRepository;
        _mailer = mailer;
        _tokenService = tokenService;
        _fileStorage = fileStorage;
    }

    public CommandResult<TokenResult> Handle(LoginCommand command)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            return new CommandResult<TokenResult>(Code.Invalid, "Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var user = _userRepository.GetByEmail(command.Email);
        if (user == null || !_hasher.Verify(command.Password, user.Password))
        {
            return new CommandResult<TokenResult>(Code.NotFound, "Usuário ou senha inválidos");
        }

        var token = _tokenService.GenerateToken(user);

        return new CommandResult<TokenResult>(Code.Ok, "Logado com sucesso!", new TokenResult(token));
    }

    public CommandResult<TokenResult> Handle(RegisterCommand command)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            return new CommandResult<TokenResult>(Code.Invalid, "Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var existingUser = _userRepository.GetByEmail(command.Email);
        if (existingUser != null)
        {
            return new CommandResult<TokenResult>(Code.Invalid, "Usuário com o email já existe");
        }

        var password = _hasher.Hash(command.Password);
        var user = new User(command.Name, command.Email, password, null);
        _userRepository.Create(user);

        var token = _tokenService.GenerateToken(user);

        return new CommandResult<TokenResult>(Code.Created, "Usuário criado com sucesso!", new TokenResult(token));
    }

    public async Task<CommandResult<User>> Handle(EditUserCommand command)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            return new CommandResult<User>(Code.Invalid, "Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        if (command.Name != null && command.Name != command.User.Name)
        {
            command.User.Name = command.Name;
        }

        if (command.Avatar != null)
        {
            command.User.AvatarUrl = await _fileStorage.SaveFileAsync(command.Avatar);
        }

        _userRepository.Update(command.User);

        return new CommandResult<User>(Code.Ok, "Usuario editado com sucesso!", command.User);
    }

    public CommandResult HandleDelete(User user)
    {
        _userRepository.Delete(user);

        return new CommandResult(Code.Ok, "Usuario deletado com sucesso!");
    }

    public CommandResult Handle(RecoverPasswordCommand command)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            return new CommandResult(Code.Invalid, "Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

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
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            return new CommandResult(Code.Invalid, "Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

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