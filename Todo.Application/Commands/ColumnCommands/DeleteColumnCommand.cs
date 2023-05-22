using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.ColumnCommands;

public class DeleteColumnValidator : AbstractValidator<DeleteColumnCommand>
{
    public DeleteColumnValidator()
    {
        RuleFor(x => x.ColumnId).NotNull().NotEmpty();
        RuleFor(x => x.User).NotNull();
    }
}

public class DeleteColumnCommand : ICommand<string>
{
    public Guid ColumnId { get; set; }
    [JsonIgnore] public User User { get; set; }

    public DeleteColumnCommand(Guid columnId, User? user)
    {
        ColumnId = columnId;
        User = user ?? new User();
    }

    public ValidationResult Validate()
    {
        var validator = new DeleteColumnValidator();
        return validator.Validate(this);
    }
}