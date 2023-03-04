using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.UserCommands;

public class ConfirmRecoverPasswordCommand : ICommand
{
    public int Code { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ConfirmRecoverPasswordCommand(int code, string password, string email)
    {
        Code = code;
        Password = password;
        Email = email;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
