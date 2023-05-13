using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.ItemCommands;

public class ChangeItemColumnValidator : AbstractValidator<ChangeItemColumnCommand>
{
    public ChangeItemColumnValidator()
    {
        RuleFor(x => x.ColumnId).NotNull().NotEmpty();
        RuleFor(x => x.ItemId).NotNull().NotEmpty();
        RuleFor(x => x.User).NotNull();
    }
}

public class ChangeItemColumnCommand : ICommand<TodoItem>
{
    public Guid ColumnId { get; init; } = Guid.Empty;
    public Guid ItemId { get; init; } = Guid.Empty;
    [JsonIgnore] public User User { get; set; }

    public ValidationResult Validate()
    {
        var validator = new ChangeItemColumnValidator();
        return validator.Validate(this);
    }
}