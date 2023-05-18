using Todo.Application.Commands.UserCommands;
using Todo.Application.Exceptions;
using Todo.Application.Handlers;
using Todo.Application.Utils;

namespace Todo.Application.Tests.HandlerTests.User;

using User = Todo.Domain.Entities.User;

public class LoginTests
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IHasher> _hasher;
    private readonly Mock<ITokenService> _tokenService;
    private readonly Fixture _fixture;
    private readonly UserHandler _handler;

    public LoginTests()
    {
        _userRepository = new Mock<IUserRepository>();
        _hasher = new Mock<IHasher>();
        Mock<IRecoverCodeRepository> recoverCodeRepository = new();
        Mock<IMailer> mailer = new();
        Mock<IFileStorage> fileStorage = new();
        _tokenService = new Mock<ITokenService>();

        _fixture = new Fixture();

        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _handler = new UserHandler(_userRepository.Object, _hasher.Object, recoverCodeRepository.Object,
            mailer.Object, _tokenService.Object, fileStorage.Object);
    }

    [Fact]
    public async Task LoginShouldCompleteTest()
    {
        var user = _fixture.Build<User>().With(
            builder => builder.Email, $"{_fixture.Create<string>()}@{_fixture.Create<string>()}"
        ).Create();

        var command = new LoginCommand(user.Email, user.Password);

        _hasher.Setup(fun => fun.Verify(user.Password, command.Password)).Returns(true);
        _userRepository.Setup(repo => repo.GetByEmail(command.Email)).ReturnsAsync(user).Verifiable();

        var token = _fixture.Create<string>();
        _tokenService.Setup(fun => fun.GenerateToken(It.IsAny<User>())).Returns(token).Verifiable();

        var result = await _handler.Handle(command, new CancellationToken());

        Assert.NotNull(result);
        Assert.Equal(token, result.Token);

        _userRepository.Verify();
        _tokenService.Verify();
    }

    [Fact]
    public async Task LoginShouldNotFound()
    {
        var user = _fixture.Build<User>().With(builder => builder.Email,
            $"{_fixture.Create<string>()}@{_fixture.Create<string>()}").Create();

        var command = new LoginCommand(user.Email, user.Password);

        _hasher.Setup(fun => fun.Verify(user.Password, command.Password)).Returns(true);
        _userRepository.Setup(repo => repo.GetByEmail(It.IsAny<string>())).ReturnsAsync(() => null).Verifiable();

        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, new CancellationToken()));
        _userRepository.Verify();
    }

    [Fact]
    public async Task LoginShouldFail()
    {
        var user = _fixture.Build<User>().With(builder => builder.Email,
            $"{_fixture.Create<string>()}@{_fixture.Create<string>()}").Create();

        var command = new LoginCommand(user.Email, user.Password);

        _hasher.Setup(fun => fun.Verify(user.Password, command.Password)).Returns(false);
        _userRepository.Setup(repo => repo.GetByEmail(command.Email)).ReturnsAsync(user).Verifiable();

        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, new CancellationToken()));
        _userRepository.Verify();
    }
}