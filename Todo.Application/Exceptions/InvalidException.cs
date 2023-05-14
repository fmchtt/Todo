using Todo.Application.Results;

namespace Todo.Application.Exceptions;

public class InvalidException : Exception
{
    private IEnumerable<ErrorResult>? Errors { get; set; }

    public InvalidException(string? message, IEnumerable<ErrorResult>? errors) : base(message)
    {
        Errors = errors;
    }
}