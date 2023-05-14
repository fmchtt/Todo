using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;

namespace Todo.Application.Commands.UserCommands;

public class ConfirmRecoverPasswordValidator : AbstractValidator<ConfirmRecoverPasswordCommand>
{
    public ConfirmRecoverPasswordValidator()
    {
        RuleFor(x => x.Code).Must(x => x > 99999);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(8);
    }
}

public class ConfirmRecoverPasswordCommand : ICommand<string>
{
    public int Code { get; set; } = 0;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        var validator = new ConfirmRecoverPasswordValidator();
        return validator.Validate(this);
    }
}