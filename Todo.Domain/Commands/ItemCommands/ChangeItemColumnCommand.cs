using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ItemCommands;

public class ChangeItemColumnValidator : AbstractValidator<ChangeItemColumnCommand>
{
    public ChangeItemColumnValidator()
    {
        RuleFor(x => x.ColumnId).NotNull().NotEmpty();
        RuleFor(x => x.ItemId).NotNull().NotEmpty();
    }
}

public class ChangeItemColumnCommand : ICommand
{
    public Guid ColumnId { get; init; } = Guid.Empty;
    public Guid ItemId { get; init; } = Guid.Empty;

    public ValidationResult Validate()
    {
        var validator = new ChangeItemColumnValidator();
        return validator.Validate(this);
    }
}