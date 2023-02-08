namespace Todo.Domain.DTO;


/*
 * Code é a propriedade que diz o tipo de sucesso obtido.
 * 200 = Ok
 * 400 = Dados incorretos
 * 401 = Sem autorização
 * 404 = Dados não encontrados 
 */

public class ResultDTO<T>
{
    public int Code { get; set; }
    public string Message { get; set; }
    public T? Result { get; set; }
    public List<ErrorDTO>? Errors { get; set; }

    public ResultDTO(int code, string message, T? result)
    {
        Code = code;
        Message = message;
        Result = result;
    }

    public ResultDTO(int code, string message, List<ErrorDTO>? errors)
    {
        Code = code;
        Message = message;
        Errors = errors;
    }

    public ResultDTO(int code, string message)
    {
        Code = code;
        Message = message;
    }
}

public class ResultDTO
{
    public int Code { get; set; }
    public string Message { get; set; }
    public List<ErrorDTO>? Errors { get; set; }

    public ResultDTO(int code, string message, List<ErrorDTO>? errors)
    {
        Code = code;
        Message = message;
        Errors = errors;
    }

    public ResultDTO(int code, string message)
    {
        Code = code;
        Message = message;
    }
}
