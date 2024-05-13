using FluentValidation;
using Todo.Application.Commands.Contracts;
using Todo.Application.Results;

namespace Todo.Application.Commands.UserCommands;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(4).Must(x => x.Contains(' '));
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}

public class RegisterCommand : ICommand<TokenResult>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public RegisterCommand(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}