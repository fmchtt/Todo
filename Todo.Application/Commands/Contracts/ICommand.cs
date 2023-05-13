using FluentValidation.Results;
using MediatR;
using Todo.Application.Handlers.Contracts;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.Contracts;

public interface ICommand<out TResult> : IRequest<TResult>
{
    public ValidationResult Validate();
}

public interface ICommand : IRequest
{
    public ValidationResult Validate();
}