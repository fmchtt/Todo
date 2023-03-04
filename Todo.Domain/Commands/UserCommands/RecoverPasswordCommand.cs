using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.UserCommands;

public class RecoverPasswordCommand : ICommand
{
    public string Email { get; set; }

    public RecoverPasswordCommand(string email)
    {
        Email = email;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
