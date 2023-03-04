using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ItemCommands;

public class CreateItemCommand : ICommand
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public Guid? BoardId { get; set; }
    public Guid? ColumnId { get; set; }

    public CreateItemCommand(string title, string description, int priority, Guid? boardId, Guid? columnId)
    {
        Title = title;
        Description = description;
        BoardId = boardId;
        ColumnId = columnId;
        Priority = priority;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
