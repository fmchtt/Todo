using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ColumnCommands;

public class DeleteColumnValidator : AbstractValidator<DeleteColumnCommand>
{
    public DeleteColumnValidator()
    {
        RuleFor(x => x.ColumnId).NotNull().NotEmpty();
    }
}

public class DeleteColumnCommand : ICommand
{
    public Guid ColumnId { get; set; } = Guid.Empty;

    public ValidationResult Validate()
    {
        var validator = new DeleteColumnValidator();
        return validator.Validate(this);
    }
}