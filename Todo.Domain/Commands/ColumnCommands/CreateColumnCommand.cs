using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ColumnCommands;

public class CreateColumnValidator : AbstractValidator<CreateColumnCommand>
{
    public CreateColumnValidator()
    {
        RuleFor(x => x.BoardId).NotNull().NotEmpty();
        RuleFor(x => x.Name).MinimumLength(5);
    }
}

public class CreateColumnCommand : ICommand
{
    public Guid BoardId { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        var validator = new CreateColumnValidator();
        return validator.Validate(this);
    }
}