using Todo.Domain.Commands.BoardCommands;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Utils;

namespace Todo.Tests.Handlers.BoardHandlers;

public class CreateBoardHandlerTests
{
    private readonly Mock<IBoardRepository> _boardRepository;
    private readonly Fixture _fixture;
    private readonly BoardHandler _handler;

    public CreateBoardHandlerTests()
    {
        _boardRepository = new Mock<IBoardRepository>();
        Mock<IInviteRepository> inviteRepository = new();
        Mock<IMailer> mailer = new();

        _fixture = new Fixture();
        
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _handler = new BoardHandler(_boardRepository.Object, inviteRepository.Object, mailer.Object);
    }

    [Fact]
    public void ShouldCreateBoard()
    {
        _boardRepository.Setup(repo => repo.Create(It.IsAny<Board>())).Verifiable();

        var user = _fixture.Create<User>();
        var command = _fixture.Create<CreateBoardCommand>();
        
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.Created, result.Code);
        
        _boardRepository.Verify();
    }
}