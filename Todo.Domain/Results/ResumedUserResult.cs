using Todo.Domain.Entities;

namespace Todo.Domain.Results;

public class ResumedUserResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? AvatarUrl { get; set; }
    
    public ResumedUserResult() {}
}
