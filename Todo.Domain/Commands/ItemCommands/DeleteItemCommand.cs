using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ItemCommands;

public class DeleteItemValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemValidator()
    {
        RuleFor(x => x.ItemId).NotNull().NotEmpty();
    }
}

public class DeleteItemCommand : ICommand
{
    public Guid ItemId { get; set; } = Guid.Empty;

    public ValidationResult Validate()
    {
        var validator = new DeleteItemValidator();
        return validator.Validate(this);
    }
}