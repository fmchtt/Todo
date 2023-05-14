using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.UserCommands;

internal class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.User).NotNull();
    }
}

public class DeleteUserCommand : ICommand<string>
{
    [JsonIgnore] public User User { get; set; }

    public DeleteUserCommand(User user)
    {
        User = user;
    }

    public ValidationResult Validate()
    {
        var validator = new DeleteUserValidator();
        return validator.Validate(this);
    }
}