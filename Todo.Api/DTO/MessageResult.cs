namespace Todo.Api.DTO;

public class MessageResult
{
    public string Message { get; set; }

    public MessageResult(string message)
    {
        Message = message;
    }
}
