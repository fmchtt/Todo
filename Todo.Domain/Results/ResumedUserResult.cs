using Todo.Domain.Entities;

namespace Todo.Domain.Results;

public class ResumedUserResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? AvatarUrl { get; set; }

    public ResumedUserResult(User user)
    {
        Id = user.Id;
        Name = user.Name;
        AvatarUrl = user.AvatarUrl;
    }
}
