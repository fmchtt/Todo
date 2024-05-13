using FluentValidation;
using System.Text.Json.Serialization;
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
}