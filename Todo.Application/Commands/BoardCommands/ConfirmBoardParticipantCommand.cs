using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.BoardCommands;

public class ConfirmBoardParticipantValidator : AbstractValidator<ConfirmBoardParticipantCommand>
{
    public ConfirmBoardParticipantValidator()
    {
        RuleFor(command => command.BoardId).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.User).NotNull();
    }
}

public class ConfirmBoardParticipantCommand : ICommand<string>
{
    public Guid BoardId { get; set; } = Guid.Empty;
    [JsonIgnore] public User User { get; set; }

    public ValidationResult Validate()
    {
        var validator = new ConfirmBoardParticipantValidator();
        return validator.Validate(this);
    }
}