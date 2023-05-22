using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.ColumnCommands;

public class CreateColumnValidator : AbstractValidator<CreateColumnCommand>
{
    public CreateColumnValidator()
    {
        RuleFor(x => x.BoardId).NotNull().NotEmpty();
        RuleFor(x => x.Name).MinimumLength(5);
        RuleFor(x => x.User).NotNull();
    }
}

public class CreateColumnCommand : ICommand<Column>
{
    public Guid BoardId { get; set; }
    public string Name { get; set; }
    [JsonIgnore] public User User { get; set; }

    public CreateColumnCommand(Guid boardId, string name, User? user)
    {
        BoardId = boardId;
        Name = name;
        User = user ?? new User();
    }

    public ValidationResult Validate()
    {
        var validator = new CreateColumnValidator();
        return validator.Validate(this);
    }
}