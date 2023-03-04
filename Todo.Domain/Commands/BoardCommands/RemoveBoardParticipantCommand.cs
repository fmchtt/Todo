using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.BoardCommands;

public class RemoveBoardParticipantCommand : ICommand
{
    public Guid BoardId { get; set; }
    public Guid ParticipantId { get; set; }
    
    public RemoveBoardParticipantCommand(Guid boardId, Guid participantId)
    {
        BoardId = boardId;
        ParticipantId = participantId;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}