using Todo.Application.Results;

namespace Todo.Application.Handlers.Contracts;

public enum Code
{
    Ok,
    Created,
    NotFound,
    Invalid,
    Unauthorized,
}

public class CommandResult<T>
{
    public Code Code { get; set; }
    public string Message { get; set; }
    public T? Result { get; set; }
    public List<ErrorResult>? Errors { get; set; }

    public CommandResult(Code code, string message, T? result)
    {
        Code = code;
        Message = message;
        Result = result;
    }

    public CommandResult(Code code, string message, List<ErrorResult>? errors)
    {
        Code = code;
        Message = message;
        Errors = errors;
    }

    public CommandResult(Code code, string message)
    {
        Code = code;
        Message = message;
    }
}

public class CommandResult
{
    public Code Code { get; set; }
    public string Message { get; set; }
    public List<ErrorResult>? Errors { get; set; }

    public CommandResult(Code code, string message, List<ErrorResult>? errors)
    {
        Code = code;
        Message = message;
        Errors = errors;
    }

    public CommandResult(Code code, string message)
    {
        Code = code;
        Message = message;
    }
}
