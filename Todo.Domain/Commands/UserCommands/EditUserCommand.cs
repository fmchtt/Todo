using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.UserCommands;

public class EditUserValidator : AbstractValidator<EditUserCommand>
{
    public EditUserValidator()
    {
        RuleFor(x => x.Name).Must(x => x == null || x.Length > 4 || x.Contains(' '));
    }
}

public class EditUserCommand : ICommand
{
    public string? Name { get; init; }
    public string? AvatarUrl { get; init; }

    public ValidationResult Validate()
    {
        var validator = new EditUserValidator();
        return validator.Validate(this);
    }
}