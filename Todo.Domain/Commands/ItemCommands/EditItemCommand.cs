using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.ItemCommands;

public class EditItemValidator : AbstractValidator<EditItemCommand>
{
    public EditItemValidator()
    {
        RuleFor(x => x.ItemId).NotNull().NotEmpty();
        RuleFor(x => x.Title).Must(x => x == null || x.Length > 5);
        RuleFor(x => x.Description).Must(x => x == null || x.Length > 10);
        When(x => x.Priority != null, () => { RuleFor(x => x.Priority).IsInEnum(); })
            .Otherwise(() => { RuleFor(x => x.Priority).Null(); });
    }
}

public class EditItemCommand : ICommand
{
    [JsonIgnore] public Guid ItemId { get; set; } = Guid.Empty;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Priority { get; set; }

    public ValidationResult Validate()
    {
        var validator = new EditItemValidator();
        return validator.Validate(this);
    }
}