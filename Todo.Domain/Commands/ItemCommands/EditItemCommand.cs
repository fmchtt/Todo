using System.Text.Json.Serialization;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ItemCommands;

public class EditItemCommand : ICommand
{
    [JsonIgnore]
    public Guid ItemId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Priority { get; set; }

    public EditItemCommand(Guid itemId, string? title, string? description, int? priority)
    {
        ItemId = itemId;
        Title = title;
        Description = description;
        Priority = priority;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}

