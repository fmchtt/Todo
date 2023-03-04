using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ItemCommands;

public class MarkValidator : AbstractValidator<MarkCommand>
{
    public MarkValidator()
    {
        RuleFor(x => x.ItemId).NotNull().NotEmpty();
        RuleFor(x => x.Done).NotNull();
    }
}

public class MarkCommand : ICommand
{
    public Guid ItemId { get; set; } = Guid.Empty;
    public bool Done { get; set; }

    public ValidationResult Validate()
    {
        var validator = new MarkValidator();
        return validator.Validate(this);
    }
}