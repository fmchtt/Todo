using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.ItemCommands;

public class EditItemValidator : AbstractValidator<EditItemCommand>
{
    public EditItemValidator()
    {
        RuleFor(x => x.ItemId).NotNull().NotEmpty();
        RuleFor(x => x.Title).MinimumLength(5);
        RuleFor(x => x.Description).MinimumLength(10);
        RuleFor(x => x.Priority).IsInEnum();
        RuleFor(x => x.User).NotNull();
    }
}

public class EditItemCommand : ICommand<TodoItem>
{
    [JsonIgnore] public Guid ItemId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public EPriority? Priority { get; set; }
    [JsonIgnore] public User User { get; set; }

    public EditItemCommand(Guid itemId, string? title, string? description, EPriority? priority, User? user)
    {
        ItemId = itemId;
        Title = title;
        Description = description;
        Priority = priority;
        User = user ?? new User();
    }

    public ValidationResult Validate()
    {
        var validator = new EditItemValidator();
        return validator.Validate(this);
    }
}