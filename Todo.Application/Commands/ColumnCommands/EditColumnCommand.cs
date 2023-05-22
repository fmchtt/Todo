using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.ColumnCommands;

public class EditColumnValidator : AbstractValidator<EditColumnCommand>
{
    public EditColumnValidator()
    {
        RuleFor(x => x.ColumnId).NotNull().NotEmpty();
        RuleFor(x => x.Name).Must(x => x == null || x.Length > 5);
        RuleFor(x => x.Order).Must(x => x is null or > 0);
        RuleFor(x => x.User).NotNull();
    }
}

public class EditColumnCommand : ICommand<Column>
{
    [JsonIgnore] public Guid ColumnId { get; set; }
    public string? Name { get; set; }
    public int? Order { get; set; }
    [JsonIgnore] public User User { get; set; }

    public EditColumnCommand(Guid columnId, string? name, int? order, User? user)
    {
        ColumnId = columnId;
        Name = name;
        Order = order;
        User = user ?? new User();
    }

    public ValidationResult Validate()
    {
        var validator = new EditColumnValidator();
        return validator.Validate(this);
    }
}