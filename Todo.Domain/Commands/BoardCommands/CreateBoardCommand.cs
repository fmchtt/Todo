using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.BoardCommands;

public class CreateBoardCommand : ICommand
{
    public string Name { get; set; }
    public string Description { get; set; }

    public CreateBoardCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
