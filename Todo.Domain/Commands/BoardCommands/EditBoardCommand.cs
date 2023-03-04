using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.BoardCommands;

public class EditBoardValidator : AbstractValidator<EditBoardCommand>
{
    public EditBoardValidator()
    {
        RuleFor(x => x.BoardId).NotNull().NotEmpty();
        RuleFor(x => x.Name).Must(x => x == null || x.Length > 5);
        RuleFor(x => x.Description).Must(x => x == null || x.Length > 10);
    }
}

public class EditBoardCommand : ICommand
{
    [JsonIgnore] public Guid BoardId { get; set; } = Guid.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }

    public ValidationResult Validate()
    {
        var validator = new EditBoardValidator();
        return validator.Validate(this);
    }
}