using Todo.Domain.Commands.BoardCommands;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Utils;

namespace Todo.Tests.Handlers.BoardHandlers;

public class AddBoardParticipantHandlerTests
{
    private readonly Mock<IBoardRepository> _boardRepository;
    private readonly Mock<IInviteRepository> _inviteRepository;
    private readonly Mock<IMailer> _mailer;
    private readonly Fixture _fixture;
    private readonly BoardHandler _handler;

    public AddBoardParticipantHandlerTests()
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
    public void ShouldInviteParticipant()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();
        board.OwnerId = user.Id;

        _boardRepository.Setup(repo => repo.GetById(board.Id)).Returns(board);
        _mailer.Setup(repo => repo.SendMail(It.IsAny<string>(), It.IsAny<string>())).Returns(new Task<bool>(() => true))
            .Verifiable();
        _inviteRepository.Setup(repo => repo.CreateMany(It.IsAny<ICollection<Invite>>())).Verifiable();

        var command = _fixture.Create<AddBoardParticipantCommand>();
        command.Emails = new List<string>
        {
            "test@test.com"
        };
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.Created, result.Code);
        _mailer.Verify();
        _inviteRepository.Verify();
    }

    [Fact]
    public void ShouldNotFindBoard()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();
        board.OwnerId = user.Id;

        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((Board) null);
        _mailer.Setup(repo => repo.SendMail(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception("SendMail method accessed"));
        _inviteRepository.Setup(repo => repo.CreateMany(It.IsAny<ICollection<Invite>>())).Throws(new Exception("Create invite method accessed"));

        var command = _fixture.Create<AddBoardParticipantCommand>();
        command.Emails = new List<string>
        {
            "test@test.com"
        };
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.NotFound, result.Code);
    }

    [Fact]
    public void ShouldNotPermitInvite()
    {
        var user = _fixture.Create<User>();
        var board = _fixture.Create<Board>();

        _boardRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(board);
        _mailer.Setup(repo => repo.SendMail(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception("SendMail method accessed"));
        _inviteRepository.Setup(repo => repo.CreateMany(It.IsAny<ICollection<Invite>>())).Throws(new Exception("Create invite method accessed"));

        var command = _fixture.Create<AddBoardParticipantCommand>();
        command.Emails = new List<string>
        {
            "test@test.com"
        };
        var result = _handler.Handle(command, user);
        
        Assert.Equal(Code.Unauthorized, result.Code);
    }
}