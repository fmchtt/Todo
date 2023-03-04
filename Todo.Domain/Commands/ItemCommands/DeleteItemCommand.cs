using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ItemCommands;

public class DeleteItemCommand : ICommand
{
    public Guid ItemId { get; set; }

    public DeleteItemCommand(Guid itemId)
    {
        ItemId = itemId;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}