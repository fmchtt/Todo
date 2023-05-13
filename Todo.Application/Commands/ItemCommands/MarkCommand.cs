using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.ItemCommands;

public class MarkValidator : AbstractValidator<MarkCommand>
{
    public MarkValidator()
    {
        RuleFor(x => x.ItemId).NotNull().NotEmpty();
        RuleFor(x => x.Done).NotNull();
        RuleFor(x => x.User).NotNull();
    }
}

public class MarkCommand : ICommand<TodoItem>
{
    public Guid ItemId { get; init; } = Guid.Empty;
    public bool Done { get; init; }
    [JsonIgnore] public User User { get; set; }

    public ValidationResult Validate()
    {
        var validator = new MarkValidator();
        return validator.Validate(this);
    }
}