using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;

namespace Todo.Domain.Handlers.Contracts;

public interface IHandler<T, R> where T : ICommand
{
    CommandResult<R> Handle(T command, User user);
}

public interface IHandler<T> where T : ICommand
{
    CommandResult Handle(T command, User user);
}

public interface IHandlerPublic<T, R> where T : ICommand
{
    CommandResult<R> Handle(T command);
}

public interface IHandlerPublic<T> where T : ICommand
{
    CommandResult Handle(T command);
}