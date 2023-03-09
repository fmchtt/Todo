using Todo.Domain.Commands.UserCommands;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Utils;

namespace Todo.Tests.Handlers.UserHandlers;

public class LoginUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IHasher> _hasher;
    private readonly Fixture _fixture;
    private readonly UserHandler _handler;

    public LoginUserHandlerTests()
    {
        _userRepository = new Mock<IUserRepository>();
        Mock<IRecoverCodeRepository> codeRepository = new();
        _hasher = new Mock<IHasher>();
        Mock<IMailer> mailer = new();

        _fixture = new Fixture();

        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _handler = new UserHandler(_userRepository.Object, _hasher.Object, codeRepository.Object, mailer.Object);
    }

    [Fact]
    public void ShouldLoginUser()
    {
        var user = _fixture.Build<User>()
            .With(builder => builder.Email, "teste@email.com")
            .With(builder => builder.Password, "12345678")
            .Create();
        
        var command = new LoginCommand
        {
            Email = user.Email,
            Password = user.Password
        };
        
        _userRepository.Setup(repo => repo.GetByEmail(It.IsAny<string>())).Returns(user).Verifiable();
        _hasher.Setup(repo => repo.Verify(command.Password, user.Password)).Returns(true).Verifiable();

        var result = _handler.Handle(command);
        
        Assert.Equal(Code.Ok, result.Code);
        _userRepository.Verify();
        _hasher.Verify();
    }
    
    [Fact]
    public void ShouldNotFindUser()
    {
        var user = _fixture.Build<User>()
            .With(builder => builder.Email, "teste@email.com")
            .With(builder => builder.Password, "12345678")
            .Create();
        
        var command = new LoginCommand
        {
            Email = user.Email,
            Password = user.Password
        };
        
        _userRepository.Setup(repo => repo.GetByEmail(It.IsAny<string>())).Returns((User) null).Verifiable();
        _hasher.Setup(repo => repo.Verify(command.Password, user.Password)).Throws(new Exception("Verify method accessed"));

        var result = _handler.Handle(command);
        
        Assert.Equal(Code.NotFound, result.Code);
        _userRepository.Verify();
    }
    
    [Fact]
    public void ShouldPermitLoginUser()
    {
        var user = _fixture.Build<User>()
            .With(builder => builder.Email, "teste@email.com")
            .With(builder => builder.Password, "12345678")
            .Create();
        
        var command = new LoginCommand
        {
            Email = user.Email,
            Password = user.Password
        };
        
        _userRepository.Setup(repo => repo.GetByEmail(It.IsAny<string>())).Returns(user).Verifiable();
        _hasher.Setup(repo => repo.Verify(command.Password, user.Password)).Returns(false).Verifiable();

        var result = _handler.Handle(command);
        
        Assert.Equal(Code.NotFound, result.Code);
        _userRepository.Verify();
        _hasher.Verify();
    }
}