namespace Todo.Domain.DTO.Input;

public class InviteDTO
{
    public List<string> Emails { get; set; }

    public InviteDTO(List<string> emails)
    {
        Emails = emails;
    }
}
