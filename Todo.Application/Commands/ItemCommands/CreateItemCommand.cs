using FluentValidation;
using System.Text.Json.Serialization;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.ItemCommands;

public class CreateItemValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemValidator()
    {
        RuleFor(x => x.Title).MinimumLength(5);
        RuleFor(x => x.Description).MinimumLength(10);
        RuleFor(x => x.Priority).IsInEnum();
        RuleFor(x => x.User).NotNull();
    }
}

public class CreateItemCommand : ICommand<TodoItem>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public EPriority Priority { get; set; }
    public Guid? BoardId { get; set; }
    public Guid? ColumnId { get; set; }
    [JsonIgnore] public User User { get; set; }

    public CreateItemCommand(string title, string description, EPriority priority, Guid? boardId, Guid? columnId, User? user)
    {
        Title = title;
        Description = description;
        Priority = priority;
        BoardId = boardId;
        ColumnId = columnId;
        User = user ?? new User();
    }
}