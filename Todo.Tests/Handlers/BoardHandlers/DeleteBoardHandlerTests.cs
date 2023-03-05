using Todo.Domain.Commands.BoardCommands;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Utils;

namespace Todo.Tests.Handlers.BoardHandlers;

public class DeleteBoardHandlerTests
{
    private readonly Mock<IBoardRepository> _boardRepository;
    private readonly Fixture _fixture;
    private readonly BoardHandler _handler;

    public DeleteBoardHandlerTests()
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
    public void ShouldDeleteBoard()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();
        board.OwnerId = user.Id;
        board.Owner = user;

        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(board);
        _boardRepository.Setup(repo => repo.Delete(It.IsAny<Board>())).Verifiable();

        var command = _fixture.Create<DeleteBoardCommand>();
        command.BoardId = board.Id;

        var result = _handler.Handle(command, user);

        Assert.Equal(Code.Ok, result.Code);

        _boardRepository.Verify();
    }

    [Fact]
    public void ShouldNotPermitDeleteBoard()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();

        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(board);
        _boardRepository.Setup(repo => repo.Delete(It.IsAny<Board>())).Throws(new Exception("Delete method accessed"));

        var command = _fixture.Create<DeleteBoardCommand>();
        command.BoardId = board.Id;

        var result = _handler.Handle(command, user);

        Assert.Equal(Code.Unauthorized, result.Code);
    }

    [Fact]
    public void ShouldNotFindDeleteBoard()
    {
        var user = _fixture.Create<User>();

        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((Board) null);
        _boardRepository.Setup(repo => repo.Delete(It.IsAny<Board>())).Throws(new Exception("Delete method accessed"));

        var command = _fixture.Create<DeleteBoardCommand>();
        command.BoardId = Guid.NewGuid();

        var result = _handler.Handle(command, user);

        Assert.Equal(Code.NotFound, result.Code);
    }
}