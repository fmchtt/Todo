using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Domain.Handlers.Contracts;

public interface IHandler<in T, TR> where T : ICommand
{
    CommandResult<TR> Handle(T command, User user);
}

public interface IHandler<in T> where T : ICommand
{
    CommandResult Handle(T command, User user);
}

public interface IHandlerPublic<in T, TR> where T : ICommand
{
    CommandResult<TR> Handle(T command);
}

public interface IHandlerPublic<in T> where T : ICommand
{
    CommandResult Handle(T command);
}