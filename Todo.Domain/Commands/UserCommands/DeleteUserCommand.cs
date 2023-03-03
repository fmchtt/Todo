using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.UserCommands;

public class DeleteUserCommand : ICommand
{
    public Guid UserId { get; set; }
    
    public bool Validate()
    {
        throw new NotImplementedException();
    }
}