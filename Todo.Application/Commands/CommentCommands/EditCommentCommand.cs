using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.CommentCommands;

public class EditCommentValidator : AbstractValidator<EditCommentCommand>
{
    public EditCommentValidator()
    {
        RuleFor(x => x.User).NotNull();
        RuleFor(x => x.CommentId).NotNull();
        RuleFor(x => x.Text).NotNull().NotEmpty();
    }
}

public class EditCommentCommand : ICommand<Comment>
{
    [JsonIgnore] public User User { get; set; }
    [JsonIgnore] public Guid CommentId { get; set; }
    public string Text { get; set; } = string.Empty;

    public EditCommentCommand()
    {
        User = new User();
    }

    public ValidationResult Validate()
    {
        var validator = new EditCommentValidator();
        return validator.Validate(this);
    }
}