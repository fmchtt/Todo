namespace Todo.Domain.DTO.Input;

public class ConfirmRecoverPasswordDTO
{
    public int Code { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ConfirmRecoverPasswordDTO(int code, string password, string email)
    {
        Code = code;
        Password = password;
        Email = email;
    }
}
