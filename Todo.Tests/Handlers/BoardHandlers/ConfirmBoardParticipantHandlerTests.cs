using Todo.Domain.Commands.BoardCommands;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Utils;

namespace Todo.Tests.Handlers.BoardHandlers;

public class ConfirmBoardParticipantHandlerTests
{
    private readonly Mock<IBoardRepository> _boardRepository;
    private readonly Mock<IInviteRepository> _inviteRepository;
    private readonly Mock<IMailer> _mailer;
    private readonly Fixture _fixture;
    private readonly BoardHandler _handler;

    public ConfirmBoardParticipantHandlerTests()
    {
        _boardRepository = new Mock<IBoardRepository>();
        _inviteRepository = new Mock<IInviteRepository>();
        _mailer = new Mock<IMailer>();

        _fixture = new Fixture();

        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _handler = new BoardHandler(_boardRepository.Object, _inviteRepository.Object, _mailer.Object);
    }

    [Fact]
    public void ShouldConfirmParticipant()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();
        var invite = _fixture.Create<Invite>();
        invite.Board = board;
        invite.Email = user.Email;

        _inviteRepository.Setup(repo => repo.GetInvite(It.IsAny<string>(), board.Id)).Returns(invite).Verifiable();
        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(board).Verifiable();
        _boardRepository.Setup(repo => repo.Update(It.IsAny<Board>())).Verifiable();

        var command = _fixture.Create<ConfirmBoardParticipantCommand>();
        command.BoardId = board.Id;
        
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.Created, result.Code);
        _inviteRepository.Verify();
        _boardRepository.Verify();
    }

    [Fact]
    public void ShouldNotFindInvite()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();

        _inviteRepository.Setup(repo => repo.GetInvite(It.IsAny<string>(), board.Id)).Returns((Invite) null).Verifiable();
        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Throws(new Exception("Get board method accessed"));
        _boardRepository.Setup(repo => repo.Update(It.IsAny<Board>())).Throws(new Exception("Update board method accessed"));

        var command = _fixture.Create<ConfirmBoardParticipantCommand>();
        command.BoardId = board.Id;
        
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.NotFound, result.Code);
        _inviteRepository.Verify();
    }

    [Fact]
    public void ShouldNotFindBoard()
    {
        var user = _fixture.Create<User>();
        var invite = _fixture.Create<Invite>();

        _inviteRepository.Setup(repo => repo.GetInvite(It.IsAny<string>(), It.IsAny<Guid>())).Returns(invite).Verifiable();
        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((Board) null);
        _boardRepository.Setup(repo => repo.Update(It.IsAny<Board>())).Throws(new Exception("Update board method accessed"));

        var command = _fixture.Create<ConfirmBoardParticipantCommand>();
        
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.NotFound, result.Code);
        _inviteRepository.Verify();
    }
}