using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.BoardCommands;

public class AddBoardParticipantCommand : ICommand
{
    public Guid BoardId { get; set; }
    public ICollection<string> Emails { get; set; }
    public string Domain { get; set; }

    public AddBoardParticipantCommand(Guid boardId, ICollection<string> emails, string domain)
    {
        BoardId = boardId;
        Emails = emails;
        Domain = domain;
    }
    
    public bool Validate()
    {
        throw new NotImplementedException();
    }
}