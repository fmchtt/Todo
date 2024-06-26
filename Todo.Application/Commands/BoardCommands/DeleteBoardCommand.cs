﻿using FluentValidation;
using System.Text.Json.Serialization;
using Todo.Application.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.BoardCommands;

public class DeleteBoardValidator : AbstractValidator<DeleteBoardCommand>
{
    public DeleteBoardValidator()
    {
        RuleFor(x => x.BoardId).NotNull().NotEmpty();
        RuleFor(x => x.User).NotNull();
    }
}

public class DeleteBoardCommand : ICommand<string>
{
    public Guid BoardId { get; set; }
    [JsonIgnore] public User User { get; set; }

    public DeleteBoardCommand(Guid boardId, User user)
    {
        BoardId = boardId;
        User = user;
    }
}