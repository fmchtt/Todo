using Todo.Domain.Entities;

namespace Todo.Domain.Results;

public class ResumedItemResult
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EPriority Priority { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public ResumedUserResult? Creator { get; set; }
    public bool Done { get; set; }
}