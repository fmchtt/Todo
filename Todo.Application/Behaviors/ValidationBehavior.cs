using FluentValidation;
using MediatR;
using Todo.Application.Commands.Contracts;
using Todo.Application.Results;

namespace Todo.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(request);
        if (!validation.IsValid)
        {
            throw new Exceptions.ValidationException(
                "Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList()
            );
        }

        return await next();
    }
}
