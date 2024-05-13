using FluentValidation;
using System.Text.Json.Serialization;
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
        RuleFor(x => x.Type).NotNull();
    }
}

public class CreateColumnCommand : ICommand<Column>
{
    public Guid BoardId { get; set; }
    public string Name { get; set; }
    public EColumnType Type { get; set; }
    [JsonIgnore] public User User { get; set; }

    public CreateColumnCommand(Guid boardId, string name, User? user, EColumnType type)
    {
        BoardId = boardId;
        Name = name;
        User = user ?? new User();
        Type = type;
    }
}