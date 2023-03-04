namespace Todo.Domain.Results;

public class ErrorResult
{
    public string Title { get; set; }
    public string Message { get; set; }

    public ErrorResult(string title, string message)
    {
        Title = title;
        Message = message;
    }
}
