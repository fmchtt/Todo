using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.BoardCommands;

public class EditBoardValidator : AbstractValidator<EditBoardCommand>
{
    public EditBoardValidator()
    {
        RuleFor(x => x.BoardId).NotNull().NotEmpty();
        RuleFor(x => x.Name).Must(x => x == null || x.Length > 5);
        RuleFor(x => x.Description).Must(x => x == null || x.Length > 10);
        RuleFor(x => x.User).NotNull();
    }
}

public class EditBoardCommand : ICommand<Board>
{
    [JsonIgnore] public Guid BoardId { get; set; } = Guid.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    [JsonIgnore] public User User { get; set; }

    public ValidationResult Validate()
    {
        var validator = new EditBoardValidator();
        return validator.Validate(this);
    }
}