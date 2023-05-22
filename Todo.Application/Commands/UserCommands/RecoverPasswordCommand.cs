using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;

namespace Todo.Application.Commands.UserCommands;

public class RecoverPasswordValidator : AbstractValidator<RecoverPasswordCommand>
{
    public RecoverPasswordValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
    }
}

public class RecoverPasswordCommand : ICommand<string>
{
    public string Email { get; set; }

    public RecoverPasswordCommand(string email)
    {
        Email = email;
    }

    public ValidationResult Validate()
    {
        var validator = new RecoverPasswordValidator();
        return validator.Validate(this);
    }
}