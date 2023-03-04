using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.BoardCommands;

public class ConfirmBoardParticipantCommand : ICommand
{
    public Guid BoardId { get; set; }

    public ConfirmBoardParticipantCommand(Guid boardId)
    {
        BoardId = boardId;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}