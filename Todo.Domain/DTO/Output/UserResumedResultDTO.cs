using Todo.Domain.Entities;

namespace Todo.Domain.DTO.Output;

public class UserResumedResultDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? AvatarUrl { get; set; }

    public UserResumedResultDTO(User user)
    {
        Id = user.Id;
        Name = user.Name;
        AvatarUrl = user.AvatarUrl;
    }
}
