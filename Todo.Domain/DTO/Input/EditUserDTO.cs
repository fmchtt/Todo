namespace Todo.Domain.DTO.Input;

public class EditUserDTO
{
    public string? Name { get; set; }
    public string? AvatarUrl { get; set; }

    public EditUserDTO(string? name, string? avatarUrl)
    {
        Name = name;
        AvatarUrl = avatarUrl;
    }
}
