namespace Todo.Domain.Commands.Contracts;

public interface ICommand
{
    public bool Validate();
}