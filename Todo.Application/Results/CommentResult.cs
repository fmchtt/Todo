namespace Todo.Application.Results;

public class CommentResult
{
    public Guid Id { get; set; }
    public ResumedUserResult Author { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
    public DateTime CreationTimeStamp { get; set; }
    public DateTime UpdateTimeStamp { get; set; }
}