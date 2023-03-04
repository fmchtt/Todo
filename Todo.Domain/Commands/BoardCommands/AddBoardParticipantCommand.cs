using System.Text.Json.Serialization;
using Todo.Domain.Commands.Contracts;
using FluentValidation;
using FluentValidation.Results;

namespace Todo.Domain.Commands.BoardCommands;

public class AddBoardParticipantValidator : AbstractValidator<AddBoardParticipantCommand>
{
    public AddBoardParticipantValidator()
    {
        RuleFor(command => command.Emails).ForEach(email => email.EmailAddress());
    }
} 

public class AddBoardParticipantCommand : ICommand
{
    [JsonIgnore] public Guid BoardId { get; set; } = Guid.Empty;
    public ICollection<string> Emails { get; set; } = new List<string>();
    [JsonIgnore]
    public string? Domain { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        var validator = new AddBoardParticipantValidator();
        return validator.Validate(this);
    }
}