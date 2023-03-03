using System.Text.Json.Serialization;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.BoardCommands;

public class EditBoardCommand : ICommand
{
    [JsonIgnore]
    public Guid BoardId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public EditBoardCommand(Guid boardId, string? name, string? description)
    {
        BoardId = boardId;
        Name = name;
        Description = description;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
