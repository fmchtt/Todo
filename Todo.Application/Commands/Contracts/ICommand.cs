using FluentValidation.Results;
using MediatR;

namespace Todo.Application.Commands.Contracts;

public interface ICommand<out TResult> : IRequest<TResult>
{
    public ValidationResult Validate();
}

public interface ICommand : IRequest
{
    public ValidationResult Validate();
}