using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ColumnCommands;

public class EditColumnValidator : AbstractValidator<EditColumnCommand>
{
    public EditColumnValidator()
    {
        RuleFor(x => x.ColumnId).NotNull().NotEmpty();
        RuleFor(x => x.Name).Must(x => x == null || x.Length > 5);
        RuleFor(x => x.Order).Must(x => x is null or > 0);
    }
}

public class EditColumnCommand : ICommand
{
    [JsonIgnore] public Guid ColumnId { get; set; } = Guid.Empty;
    public string? Name { get; set; }
    public int? Order { get; set; }

    public ValidationResult Validate()
    {
        var validator = new EditColumnValidator();
        return validator.Validate(this);
    }
}