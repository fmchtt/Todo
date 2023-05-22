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
    public Guid ColumnId { get; init; }
    public Guid ItemId { get; init; }
    [JsonIgnore] public User User { get; set; }

    public ChangeItemColumnCommand(Guid columnId, Guid itemId, User? user)
    {
        ColumnId = columnId;
        ItemId = itemId;
        User = user ?? new User();
    }

    public ValidationResult Validate()
    {
        var validator = new ChangeItemColumnValidator();
        return validator.Validate(this);
    }
}