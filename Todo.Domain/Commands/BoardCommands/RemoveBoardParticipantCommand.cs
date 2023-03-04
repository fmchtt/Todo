using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.BoardCommands;

public class RemoveBoardParticipantValidator : AbstractValidator<RemoveBoardParticipantCommand>
{
    public RemoveBoardParticipantValidator()
    {
        RuleFor(x => x.BoardId).NotNull().NotEmpty();
        RuleFor(x => x.ParticipantId).NotNull().NotEmpty();
    }
}

public class RemoveBoardParticipantCommand : ICommand
{
    public Guid BoardId { get; init; } = Guid.Empty;
    public Guid ParticipantId { get; init; } = Guid.Empty;

    public ValidationResult Validate()
    {
        var validator = new RemoveBoardParticipantValidator();
        return validator.Validate(this);
    }
}