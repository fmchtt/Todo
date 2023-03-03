using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ColumnCommands;

public class DeleteColumnCommand : ICommand
{
    public Guid ColumnId { get; set; }

    public DeleteColumnCommand(Guid columnId)
    {
        ColumnId = columnId;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}