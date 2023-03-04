using Todo.Domain.Entities;

namespace Todo.Domain.Results;

public class ResumedBoardResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ItemCount { get; set; }
    public int DoneItemCount { get; set; }

    public ResumedBoardResult()
    {
    }
}