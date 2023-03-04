using FluentValidation.Results;

namespace Todo.Domain.Results;

public class ErrorResult
{
    public string Title { get; set; }
    public string Message { get; set; }

    public ErrorResult(string title, string message)
    {
        Title = title;
        Message = message;
    }

    public ErrorResult(ValidationFailure error)
    {
        Title = error.PropertyName;
        Message = error.ErrorMessage;
    }
}
