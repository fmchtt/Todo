using System.Text.Json.Serialization;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ColumnCommands;

public class EditColumnCommand : ICommand
{
    [JsonIgnore]
    public Guid ColumnId { get; set; }
    public string? Name { get; set; }
    public int? Order { get; set; }

    public EditColumnCommand(Guid columnId, string? name, int? order)
    {
        Name = name;
        Order = order;
        ColumnId = columnId;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
