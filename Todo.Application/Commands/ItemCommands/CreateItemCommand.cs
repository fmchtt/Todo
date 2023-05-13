using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.ItemCommands;

public class CreateItemValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemValidator()
    {
        RuleFor(x => x.Title).MinimumLength(5);
        RuleFor(x => x.Description).MinimumLength(10);
        RuleFor(x => x.Priority).IsInEnum();
        RuleFor(x => x.User).NotNull();
    }
}

public class CreateItemCommand : ICommand<TodoItem>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EPriority Priority { get; set; } = 0;
    public Guid? BoardId { get; set; }
    public Guid? ColumnId { get; set; }
    [JsonIgnore] public User User { get; set; }

    public ValidationResult Validate()
    {
        var validator = new CreateItemValidator();
        return validator.Validate(this);
    }
}