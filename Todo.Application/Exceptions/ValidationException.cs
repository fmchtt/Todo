using Todo.Application.Results;

namespace Todo.Application.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<ErrorResult>? Errors { get; set; }

    public ValidationException(string? message, IEnumerable<ErrorResult>? errors) : base(message)
    {
        Errors = errors;
    }
}