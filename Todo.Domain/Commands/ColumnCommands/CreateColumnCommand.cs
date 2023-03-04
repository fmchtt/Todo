using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ColumnCommands;

public class CreateColumnCommand : ICommand
{
    public string Name { get; set; }
    public Guid BoardId { get; set; }

    public CreateColumnCommand(string name, Guid boardId)
    {
        Name = name;
        BoardId = boardId;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
