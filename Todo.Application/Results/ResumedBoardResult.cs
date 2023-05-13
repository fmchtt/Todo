namespace Todo.Application.Results;

public class ResumedBoardResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ItemCount { get; set; }
    public int DoneItemCount { get; set; }
}