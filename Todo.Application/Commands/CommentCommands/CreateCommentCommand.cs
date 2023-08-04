using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.CommentCommands;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.Text).NotNull().NotEmpty();
        RuleFor(x => x.User).NotNull();
        RuleFor(x => x.ItemId).NotNull();
    }
}

public class CreateCommentCommand : ICommand<Comment>
{
    [JsonIgnore] public User User { get; set; }
    [JsonIgnore] public Guid ItemId { get; set; }
    public string Text { get; set; } = string.Empty;

    public CreateCommentCommand()
    {
        User = new User();
    }

    public CreateCommentCommand(User user, Guid itemId, string text)
    {
        User = user;
        ItemId = itemId;
        Text = text;
    }

    public ValidationResult Validate()
    {
        var validator = new CreateCommentValidator();
        return validator.Validate(this);
    }
}