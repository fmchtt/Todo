using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ItemCommands;

public class ChangeItemColumnCommand : ICommand
{
    public Guid ColumnId { get; set; }
    public Guid ItemId { get; set; }

    public ChangeItemColumnCommand(Guid columnId, Guid itemId)
    {
        ColumnId = columnId;
        ItemId = itemId;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}