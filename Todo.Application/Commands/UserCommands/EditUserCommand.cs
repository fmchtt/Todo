using FluentValidation;
using System.Text.Json.Serialization;
using Todo.Application.Commands.Contracts;
using Todo.Application.DTO;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.UserCommands;

public class EditUserValidator : AbstractValidator<EditUserCommand>
{
    public EditUserValidator()
    {
        RuleFor(x => x.Name).MinimumLength(4).Must(x => x.Contains(' '));
        RuleFor(x => x.User).NotNull();
    }
}

public class EditUserCommand : ICommand<User>
{
    public string? Name { get; init; }
    public UploadedFile? Avatar { get; init; }
    [JsonIgnore] public User User { get; set; }

    public EditUserCommand(string? name, UploadedFile? avatar, User? user)
    {
        Name = name;
        Avatar = avatar;
        User = user ?? new User();
    }
}