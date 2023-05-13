using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
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
    [JsonIgnore] public Guid BoardId { get; set; } = Guid.Empty;
    public ICollection<string> Emails { get; set; } = new List<string>();
    [JsonIgnore]
    public string? Domain { get; set; } = string.Empty;
    
    [JsonIgnore] public User User { get; set; }

    public ValidationResult Validate()
    {
        var validator = new AddBoardParticipantValidator();
        return validator.Validate(this);
    }
}