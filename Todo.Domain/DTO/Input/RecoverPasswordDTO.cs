namespace Todo.Domain.DTO.Input;

public class RecoverPasswordDTO
{
    public string Email { get; set; }

    public RecoverPasswordDTO(string email)
    {
        Email = email;
    }
}
