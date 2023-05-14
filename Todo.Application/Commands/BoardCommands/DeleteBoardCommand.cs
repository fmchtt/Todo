using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.BoardCommands;

public class DeleteBoardValidator : AbstractValidator<DeleteBoardCommand>
{
    public DeleteBoardValidator()
    {
        RuleFor(x => x.BoardId).NotNull().NotEmpty();
        RuleFor(x => x.User).NotNull();
    }
}

public class DeleteBoardCommand : ICommand<string>
{
    public Guid BoardId { get; set; } = Guid.Empty;
    [JsonIgnore] public User User { get; set; }

    public ValidationResult Validate()
    {
        var validator = new DeleteBoardValidator();
        return validator.Validate(this);
    }
}