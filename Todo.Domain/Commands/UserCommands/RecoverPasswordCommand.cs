using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.UserCommands;

public class RecoverPasswordValidator : AbstractValidator<RecoverPasswordCommand>
{
    public RecoverPasswordValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
    }
}

public class RecoverPasswordCommand : ICommand
{
    public string Email { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        var validator = new RecoverPasswordValidator();
        return validator.Validate(this);
    }
}