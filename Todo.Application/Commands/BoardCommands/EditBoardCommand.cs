using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.BoardCommands;

public class EditBoardValidator : AbstractValidator<EditBoardCommand>
{
    public EditBoardValidator()
    {
        RuleFor(x => x.BoardId).NotNull().NotEmpty();
        RuleFor(x => x.Name).MinimumLength(5);
        RuleFor(x => x.Description).MinimumLength(10);
        RuleFor(x => x.User).NotNull();
    }
}

public class EditBoardCommand : ICommand<Board>
{
    [JsonIgnore] public Guid BoardId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    [JsonIgnore] public User User { get; set; }

    public EditBoardCommand(Guid boardId, string? name, string? description, User? user)
    {
        BoardId = boardId;
        Name = name;
        Description = description;
        User = user ?? new User();
    }

    public ValidationResult Validate()
    {
        var validator = new EditBoardValidator();
        return validator.Validate(this);
    }
}