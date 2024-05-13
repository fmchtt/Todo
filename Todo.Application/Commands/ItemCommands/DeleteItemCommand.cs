using FluentValidation;
using System.Text.Json.Serialization;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.ItemCommands;

public class DeleteItemValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemValidator()
    {
        RuleFor(x => x.ItemId).NotNull().NotEmpty();
        RuleFor(x => x.User).NotNull();
    }
}

public class DeleteItemCommand : ICommand<string>
{
    public Guid ItemId { get; init; }
    [JsonIgnore] public User User { get; set; }

    public DeleteItemCommand(Guid itemId, User? user)
    {
        ItemId = itemId;
        User = user ?? new User();
    }
}