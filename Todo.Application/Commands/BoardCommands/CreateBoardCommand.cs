using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.BoardCommands;

public class CreateBoardValidator : AbstractValidator<CreateBoardCommand>
{
    public CreateBoardValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(5);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(10);
        RuleFor(x => x.User).NotNull();
    }
}

public class CreateBoardCommand : ICommand<Board>
{
    public string Name { get; set; }
    public string Description { get; set; }
    [JsonIgnore] public User User { get; set; }

    public CreateBoardCommand(string name, string description, User? user)
    {
        Name = name;
        Description = description;
        User = user ?? new User();
    }

    public ValidationResult Validate()
    {
        var validator = new CreateBoardValidator();
        return validator.Validate(this);
    }
}