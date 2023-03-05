using Todo.Domain.Commands.ColumnCommands;
using Todo.Domain.Handlers.Contracts;

namespace Todo.Tests.Handlers.ColumnHandlers;

public class CreateColumnHandlerTests
{
    private readonly Mock<IBoardRepository> _boardRepository;
    private readonly Mock<IColumnRepository> _columnRepository;
    private readonly Fixture _fixture;
    private readonly ColumnHandler _handler;

    public CreateColumnHandlerTests()
    {
        _boardRepository = new Mock<IBoardRepository>();
        _columnRepository = new Mock<IColumnRepository>();

        _fixture = new Fixture();
        
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _handler = new ColumnHandler(_boardRepository.Object, _columnRepository.Object);
    }

    [Fact]
    public void ShouldCreateColumn()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();
        board.OwnerId = user.Id;
        board.Owner = user;
        board.Participants = new List<User>
        {
            user
        };

        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(board).Verifiable();
        _columnRepository.Setup(repo => repo.Create(It.IsAny<Column>())).Verifiable();
        _columnRepository.Setup(repo => repo.GetMaxOrder(It.IsAny<Guid>())).Returns(1).Verifiable();

        var command = _fixture.Create<CreateColumnCommand>();
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.Created, result.Code);
        _columnRepository.Verify();
    }
    
    [Fact]
    public void ShouldNotFindBoard()
    {
        var user = _fixture.Create<User>();

        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((Board) null).Verifiable();
        _columnRepository.Setup(repo => repo.Create(It.IsAny<Column>())).Throws(new Exception("Create method accessed"));
        _columnRepository.Setup(repo => repo.GetMaxOrder(It.IsAny<Guid>())).Throws(new Exception("Create method accessed"));

        var command = _fixture.Create<CreateColumnCommand>();
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.NotFound, result.Code);
        _columnRepository.Verify();
    }
    
    [Fact]
    public void ShouldNotPermitCreateColumn()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();

        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(board).Verifiable();
        _columnRepository.Setup(repo => repo.Create(It.IsAny<Column>())).Throws(new Exception("Create method accessed"));
        _columnRepository.Setup(repo => repo.GetMaxOrder(It.IsAny<Guid>())).Throws(new Exception("Create method accessed"));

        var command = _fixture.Create<CreateColumnCommand>();
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.Unauthorized, result.Code);
        _columnRepository.Verify();
    }
}