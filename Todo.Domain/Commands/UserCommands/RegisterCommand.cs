using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.UserCommands;

public class RegisterCommand : ICommand
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public RegisterCommand(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
