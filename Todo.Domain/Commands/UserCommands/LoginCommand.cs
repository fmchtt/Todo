using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.UserCommands;

public class LoginCommand : ICommand
{
    public string Email { get; set; }
    public string Password { get; set; }

    public LoginCommand(string email, string password) {
        Email = email;
        Password = password;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
