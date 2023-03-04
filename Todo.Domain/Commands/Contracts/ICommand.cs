using FluentValidation.Results;

namespace Todo.Domain.Commands.Contracts;

public interface ICommand
{
    public ValidationResult Validate();
}