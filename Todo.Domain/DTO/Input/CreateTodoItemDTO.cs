namespace Todo.Domain.DTO.Input;

public class CreateTodoItemDTO
{
    public string Title { get; set; }
    public string Description { get; set; }

    public CreateTodoItemDTO(string title, string description) { 
        Title = title;
        Description = description;
    }   
}
