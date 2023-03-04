using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.BoardCommands;

public class DeleteBoardValidator : AbstractValidator<DeleteBoardCommand>
{
    public DeleteBoardValidator()
    {
        RuleFor(x => x.BoardId).NotNull().NotEmpty();
    }
}

public class DeleteBoardCommand : ICommand
{
    public Guid BoardId { get; init; } = Guid.Empty;

    public ValidationResult Validate()
    {
        var validator = new DeleteBoardValidator();
        return validator.Validate(this);
    }
}