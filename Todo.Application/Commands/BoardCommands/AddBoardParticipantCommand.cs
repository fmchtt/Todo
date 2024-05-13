using FluentValidation;
using System.Text.Json.Serialization;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.BoardCommands;

public class AddBoardParticipantValidator : AbstractValidator<AddBoardParticipantCommand>
{
    public AddBoardParticipantValidator()
    {
        RuleFor(command => command.Emails).ForEach(email => email.EmailAddress());
        RuleFor(x => x.User).NotNull();
    }
}

public class AddBoardParticipantCommand : ICommand<string>
{
    [JsonIgnore] public Guid BoardId { get; set; }
    public ICollection<string> Emails { get; set; }
    [JsonIgnore] public string? Domain { get; set; }

    [JsonIgnore] public User User { get; set; }

    public AddBoardParticipantCommand(Guid boardId, ICollection<string>? emails, string? domain, User? user)
    {
        BoardId = boardId;
        Domain = domain;
        Emails = emails ?? new List<string>();
        User = user ?? new User();
    }
}