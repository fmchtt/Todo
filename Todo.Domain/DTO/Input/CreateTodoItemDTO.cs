namespace Todo.Domain.DTO.Input;

public class CreateTodoItemDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public Guid? BoardId { get; set; }
    public Guid? ColumnId { get; set; }

    public CreateTodoItemDTO(string title, string description, int priority, Guid? boardId, Guid? columnId)
    {
        Title = title;
        Description = description;
        BoardId = boardId;
        ColumnId = columnId;
        Priority = priority;
    }
}
