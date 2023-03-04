using System.Text.Json.Serialization;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.BoardCommands;

public class AddBoardParticipantCommand : ICommand
{
    [JsonIgnore]
    public Guid BoardId { get; set; }
    public ICollection<string> Emails { get; set; }
    [JsonIgnore]
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