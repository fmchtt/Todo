using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Application.Results;

namespace Todo.Application.Commands.UserCommands;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}

public class LoginCommand : ICommand<TokenResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
    
    public LoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public ValidationResult Validate()
    {
        var validator = new LoginValidator();
        return validator.Validate(this);
    }
}