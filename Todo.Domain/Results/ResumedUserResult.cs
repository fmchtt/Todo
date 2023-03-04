namespace Todo.Domain.Results;

public class ResumedUserResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
}