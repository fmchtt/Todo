using FluentValidation;
using System.Text.Json.Serialization;
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

    public Guid BoardId { get; set; }
    [JsonIgnore] public User User { get; set; }

    public ConfirmBoardParticipantCommand(Guid boardId, User? user)
    {
        BoardId = boardId;
        User = user ?? new User();
    }
}