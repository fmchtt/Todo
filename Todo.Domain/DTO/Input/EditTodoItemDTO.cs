namespace Todo.Domain.DTO.Input;

public class EditTodoItemDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Priority { get; set; }

    public EditTodoItemDTO(string? title, string? description, int? priority)
    {
        Title = title;
        Description = description;
        Priority = priority;
    }
}

