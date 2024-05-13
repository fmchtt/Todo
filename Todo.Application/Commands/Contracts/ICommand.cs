using MediatR;

namespace Todo.Application.Commands.Contracts;

public interface ICommand<out TResult> : IRequest<TResult>
{
}

public interface ICommand : IRequest
{
}