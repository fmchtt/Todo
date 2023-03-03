using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.UserCommands;

public class EditUserCommand : ICommand
{
    public string? Name { get; set; }
    public string? AvatarUrl { get; set; }

    public EditUserCommand(string? name, string? avatarUrl)
    {
        Name = name;
        AvatarUrl = avatarUrl;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
