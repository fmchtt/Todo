using MediatR;
using Todo.Application.Commands.UserCommands;
using Todo.Application.Exceptions;
using Todo.Application.Queries;
using Todo.Application.Results;
using Todo.Application.Utils;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Application.Handlers;

public class UserHandler : IRequestHandler<LoginCommand, TokenResult>, IRequestHandler<RegisterCommand, TokenResult>,
    IRequestHandler<EditUserCommand, User>, IRequestHandler<RecoverPasswordCommand, string>,
    IRequestHandler<ConfirmRecoverPasswordCommand, string>, IRequestHandler<DeleteUserCommand, string>, IRequestHandler<GetConfirmationCodeQuery, string>
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

    public async Task<TokenResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var user = await _userRepository.GetByEmail(command.Email);
        if (user == null || !_hasher.Verify(command.Password, user.Password))
        {
            throw new NotFoundException("Usuário ou senha inválidos");
        }

        var token = _tokenService.GenerateToken(user);

        return new TokenResult(token);
    }

    public async Task<TokenResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var existingUser = await _userRepository.GetByEmail(command.Email);
        if (existingUser != null)
        {
            throw new ValidationException("Usuário com o email já existe", null);
        }

        var password = _hasher.Hash(command.Password);
        var user = new User(command.Name, command.Email, password, null);
        await _userRepository.Create(user);

        var token = _tokenService.GenerateToken(user);

        return new TokenResult(token);
    }

    public async Task<User> Handle(EditUserCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        if (command.Name != null && command.Name != command.User.Name)
        {
            command.User.Name = command.Name;
        }

        if (command.Avatar != null)
        {
            var avatarUrl = await _fileStorage.SaveFileAsync(command.Avatar);
            command.User.AvatarUrl = avatarUrl;
        }

        await _userRepository.Update(command.User);

        return command.User;
    }

    public async Task<string> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        await _userRepository.Delete(command.User);

        return "Usuario deletado com sucesso!";
    }

    public async Task<string> Handle(RecoverPasswordCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var user = await _userRepository.GetByEmail(command.Email);
        if (user == null)
        {
            throw new NotFoundException("Usuário não encontrado!");
        }

        int code;
        var existingRecoverCode = await _recoverCodeRepository.Get(command.Email);
        if (existingRecoverCode == null)
        {
            code = new Random().Next(100000, 999999);

            var recoverCode = new RecoverCode(user.Id, code);
            await _recoverCodeRepository.Create(recoverCode);
        }
        else
        {
            code = existingRecoverCode.Code;
        }

        await _mailer.SendMail(user.Email, $"Seu código de recuperação de senha: {code}.");

        return "Enviado com sucesso!";
    }

    public async Task<string> Handle(ConfirmRecoverPasswordCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var code = await _recoverCodeRepository.Get(command.Code, command.Email);
        if (code == null)
        {
            throw new NotFoundException("Usuário não encontrado!");
        }

        code.User.Password = _hasher.Hash(command.Password);
        await _userRepository.Update(code.User);

        await _recoverCodeRepository.Delete(code);

        return "Senha alterada com sucesso!";
    }

    public async Task<string> Handle(GetConfirmationCodeQuery query, CancellationToken cancellationToken)
    {
        var code = await _recoverCodeRepository.Get(query.Code, query.Email);

        if (code == null)
        {
            throw new NotFoundException("Código inexistente!");
        }

        return "Código validado!";
    }
}