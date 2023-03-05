using Todo.Domain.Commands.BoardCommands;
using Todo.Domain.Handlers.Contracts;
using Todo.Tests.Utils;

namespace Todo.Tests.Handlers.BoardHandlers;

public class EditBoardHandlerTests
{
    private readonly Mock<IBoardRepository> _boardRepository;
    private readonly Fixture _fixture;
    private readonly BoardHandler _handler;

    public EditBoardHandlerTests()
    {
        _boardRepository = new Mock<IBoardRepository>();
        Mock<IInviteRepository> inviteRepository = new();

        _fixture = new Fixture();

        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _handler = new BoardHandler(_boardRepository.Object, inviteRepository.Object, new FakeMailer());
    }

    [Fact]
    public void ShouldEditBoard()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();
        board.OwnerId = user.Id;
        board.Participants = new List<User>
        {
            user
        };

        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(board);
        _boardRepository.Setup(repo => repo.Update(It.IsAny<Board>())).Verifiable();

        var command = _fixture.Create<EditBoardCommand>();
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.Ok, result.Code);
        _boardRepository.Verify();
    }

    [Fact]
    public void ShouldNotPermitEditBoard()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();

        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(board);
        _boardRepository.Setup(repo => repo.Create(It.IsAny<Board>())).Throws(new Exception("Update method accessed"));

        var command = _fixture.Create<EditBoardCommand>();
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.Unauthorized, result.Code);
    }

    [Fact]
    public void ShouldNotFindEditBoard()
    {
        var user = _fixture.Create<User>();

        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((Board) null);
        _boardRepository.Setup(repo => repo.Create(It.IsAny<Board>())).Throws(new Exception("Update method accessed"));

        var command = _fixture.Create<EditBoardCommand>();
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.NotFound, result.Code);
    }
}