using FluentValidation;
using System.Text.Json.Serialization;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.BoardCommands;

public class RemoveBoardParticipantValidator : AbstractValidator<RemoveBoardParticipantCommand>
{
    public RemoveBoardParticipantValidator()
    {
        RuleFor(x => x.BoardId).NotNull().NotEmpty();
        RuleFor(x => x.ParticipantId).NotNull().NotEmpty();
        RuleFor(x => x.User).NotNull();
    }
}

public class RemoveBoardParticipantCommand : ICommand<string>
{
    public Guid BoardId { get; init; }
    public Guid ParticipantId { get; init; }
    [JsonIgnore] public User User { get; set; }

    public RemoveBoardParticipantCommand(Guid boardId, Guid participantId, User? user)
    {
        BoardId = boardId;
        ParticipantId = participantId;
        User = user ?? new User();
    }
}