using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.CommentCommands;

public class DeleteCommentValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentValidator()
    {
        RuleFor(x => x.User).NotNull();
        RuleFor(x => x.CommentId).NotNull();
    }
}

public class DeleteCommentCommand : ICommand<string>
{
    [JsonIgnore] public User User { get; set; }
    [JsonIgnore] public Guid CommentId { get; set; }

    public DeleteCommentCommand(User user, Guid commentId)
    {
        User = user;
        CommentId = commentId;
    }

    public ValidationResult Validate()
    {
        var validator = new DeleteCommentValidator();
        return validator.Validate(this);
    }
}