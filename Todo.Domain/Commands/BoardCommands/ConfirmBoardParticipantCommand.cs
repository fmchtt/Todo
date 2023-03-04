using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.BoardCommands;

public class ConfirmBoardParticipantValidator : AbstractValidator<ConfirmBoardParticipantCommand>
{
    public ConfirmBoardParticipantValidator()
    {
        RuleFor(command => command.BoardId).NotEmpty().NotEqual(Guid.Empty);
    }
}

public class ConfirmBoardParticipantCommand : ICommand
{
    public Guid BoardId { get; init; } = Guid.Empty;

    public ValidationResult Validate()
    {
        var validator = new ConfirmBoardParticipantValidator();
        return validator.Validate(this);
    }
}