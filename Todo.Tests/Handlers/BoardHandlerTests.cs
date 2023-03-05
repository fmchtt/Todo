using Todo.Domain.Commands.BoardCommands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;
using Todo.Tests.Utils;

namespace Todo.Tests.Handlers;

public class BoardHandlerTests
{
    private readonly Mock<IBoardRepository> _boardRepository;
    private readonly Mock<IInviteRepository> _inviteRepository;
    private readonly Fixture _fixture;
    private readonly BoardHandler _handler;

    public BoardHandlerTests()
    {
        _boardRepository = new Mock<IBoardRepository>();
        _inviteRepository = new Mock<IInviteRepository>();

        _fixture = new Fixture();
        
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _handler = new BoardHandler(_boardRepository.Object, _inviteRepository.Object, new FakeMailer());
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
    public void ShouldntDeleteBoard()
    {
        
    }
}