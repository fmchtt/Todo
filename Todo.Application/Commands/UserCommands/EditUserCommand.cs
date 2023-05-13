using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.UserCommands;

public class EditUserValidator : AbstractValidator<EditUserCommand>
{
    public EditUserValidator()
    {
        RuleFor(x => x.Name).Must(x => x == null || x.Length > 4 || x.Contains(' '));
        RuleFor(x => x.User).NotNull();
    }
}

public class EditUserCommand : ICommand<User>
{
    public string? Name { get; init; }
    public string? AvatarUrl { get; init; }
    [JsonIgnore] public User User { get; set; }

    public EditUserCommand(string? name, string? avatarUrl, User user)
    {
        Name = name;
        AvatarUrl = avatarUrl;
        User = user;
    }

    public ValidationResult Validate()
    {
        var validator = new EditUserValidator();
        return validator.Validate(this);
    }
}