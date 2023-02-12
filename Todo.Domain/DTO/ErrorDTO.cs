namespace Todo.Domain.DTO;

public class ErrorDTO
{
    public string Title { get; set; }
    public string Message { get; set; }

    public ErrorDTO(string title, string message)
    {
        Title = title;
        Message = message;
    }
}
