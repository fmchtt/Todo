using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.ColumnCommands;

public class EditColumnValidator : AbstractValidator<EditColumnCommand>
{
    public EditColumnValidator()
    {
        RuleFor(x => x.ColumnId).NotNull().NotEmpty();
        RuleFor(x => x.Name).MinimumLength(5);
        RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
        RuleFor(x => x.User).NotNull();
    }
}

public class EditColumnCommand : ICommand<Column>
{
    [JsonIgnore] public Guid ColumnId { get; set; }
    public string? Name { get; set; }
    public int? Order { get; set; }
    public EColumnType? Type { get; set; }
    [JsonIgnore] public User User { get; set; }

    public EditColumnCommand(Guid columnId, string? name, int? order, User? user, EColumnType? type)
    {
        ColumnId = columnId;
        Name = name;
        Order = order;
        User = user ?? new User();
        Type = type;
    }

    public ValidationResult Validate()
    {
        var validator = new EditColumnValidator();
        return validator.Validate(this);
    }
}