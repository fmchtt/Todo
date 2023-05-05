using Todo.Domain.Commands.UserCommands;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Utils;

namespace Todo.Tests.Handlers.UserHandlers;

public class RegisterUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IHasher> _hasher;
    private readonly Fixture _fixture;
    private readonly UserHandler _handler;

    public RegisterUserHandlerTests()
    {
        _userRepository = new Mock<IUserRepository>();
        _hasher = new Mock<IHasher>();
        Mock<IMailer> mailer = new();
        Mock<ITokenService> tokenService = new();
        Mock<IRecoverCodeRepository> codeRepository = new();
        Mock<IFileStorage> fileStorage = new();

        _fixture = new Fixture();

        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _handler = new UserHandler(_userRepository.Object, _hasher.Object, codeRepository.Object, mailer.Object,
            tokenService.Object, fileStorage.Object);
    }

    [Fact]
    public void ShouldRegisterUser()
    {
        var command = new RegisterCommand
        {
            Email = "teste@teste.com",
            Name = "Usuário Teste",
            Password = "12345678"
        };

        _userRepository.Setup(repo => repo.GetByEmail(command.Email)).Returns((User)null).Verifiable();
        _hasher.Setup(repo => repo.Hash(command.Password)).Returns(command.Password).Verifiable();

        var result = _handler.Handle(command);

        Assert.Equal(Code.Created, result.Code);

        _userRepository.Verify();
        _hasher.Verify();
    }

    [Fact]
    public void ShouldNotRegisterUser()
    {
        var user = _fixture.Create<User>();

        var command = new RegisterCommand
        {
            Email = "teste@teste.com",
            Name = "Usuário Teste",
            Password = "12345678"
        };

        _userRepository.Setup(repo => repo.GetByEmail(command.Email)).Returns(user).Verifiable();
        _hasher.Setup(repo => repo.Hash(command.Password)).Throws(new Exception("Hash method accessed"));

        var result = _handler.Handle(command);

        Assert.Equal(Code.Invalid, result.Code);

        _userRepository.Verify();
    }
}