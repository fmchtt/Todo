namespace Todo.Application.Results;

public class CommentResult
{
    public Guid Id { get; set; }
    public ResumedUserResult Author { get; set; }
    public string Text { get; set; }
    public DateTime CreationTimeStamp { get; set; }
    public DateTime UpdateTimeStamp { get; set; }
}