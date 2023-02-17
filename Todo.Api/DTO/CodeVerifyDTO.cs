namespace Todo.Api.DTO;

public class CodeVerifyDTO
{
    public string Email { get; set; }
    public int Code { get; set; }

    public CodeVerifyDTO(string email, int code)
    {
        Email = email;
        Code = code;
    }
}
