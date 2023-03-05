using Todo.Domain.Commands.BoardCommands;
using Todo.Domain.Commands.ColumnCommands;
using Todo.Domain.Handlers.Contracts;

namespace Todo.Tests.Handlers.ColumnHandlers;

public class DeleteColumnHandlerTests
{
    private readonly Mock<IColumnRepository> _columnRepository;
    private readonly Fixture _fixture;
    private readonly ColumnHandler _handler;

    public DeleteColumnHandlerTests()
    {
        Mock<IBoardRepository> boardRepository = new();
        _columnRepository = new Mock<IColumnRepository>();

        _fixture = new Fixture();
        
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _handler = new ColumnHandler(boardRepository.Object, _columnRepository.Object);
    }

    [Fact]
    public void ShouldDeleteColumn()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();
        board.Owner = user;
        board.OwnerId = user.Id;
        board.Participants = new List<User>
        {
            user
        };
        var column = _fixture.Create<Column>();
        column.BoardId = board.Id;
        column.Board = board;

        _columnRepository.Setup(repo => repo.GetById(column.Id)).Returns(column).Verifiable();
        _columnRepository.Setup(repo => repo.Delete(column)).Verifiable();
        _columnRepository.Setup(repo => repo.ColumnReorder(board.Id)).Verifiable();

        var command = _fixture.Create<DeleteColumnCommand>();
        command.ColumnId = column.Id;

        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.Ok, result.Code);
        _columnRepository.Verify();
    }

    [Fact]
    public void ShouldNotFindColumn()
    {
        var user = _fixture.Create<User>();

        _columnRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((Column) null).Verifiable();
        _columnRepository.Setup(repo => repo.Delete(It.IsAny<Column>())).Throws(new Exception("Delete method accessed"));
        _columnRepository.Setup(repo => repo.ColumnReorder(It.IsAny<Guid>())).Throws(new Exception("ColumnReorder method accessed"));

        var command = _fixture.Create<DeleteColumnCommand>();

        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.NotFound, result.Code);
        _columnRepository.Verify();
    }

    [Fact]
    public void ShouldNotPermitDeleteColumn()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();
        var column = _fixture.Create<Column>();
        column.BoardId = board.Id;
        column.Board = board;

        _columnRepository.Setup(repo => repo.GetById(column.Id)).Returns(column).Verifiable();
        _columnRepository.Setup(repo => repo.Delete(It.IsAny<Column>())).Throws(new Exception("Delete method accessed"));
        _columnRepository.Setup(repo => repo.ColumnReorder(It.IsAny<Guid>())).Throws(new Exception("ColumnReorder method accessed"));

        var command = _fixture.Create<DeleteColumnCommand>();
        command.ColumnId = column.Id;

        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.Unauthorized, result.Code);
        _columnRepository.Verify();
    }
}