using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ItemCommands;

public class MarkCommand : ICommand
{
    public Guid ItemId { get; set; }
    public bool Done { get; set; }
    
    public MarkCommand(Guid itemId, bool done)
    {
        ItemId = itemId;
        Done = done;
    }
    
    public bool Validate()
    {
        throw new NotImplementedException();
    }
}