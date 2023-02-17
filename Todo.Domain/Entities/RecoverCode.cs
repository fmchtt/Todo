namespace Todo.Domain.Entities;

public class RecoverCode : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int Code { get; set; }

    #pragma warning disable CS8618
    public RecoverCode(Guid userId, int code)
    {
        UserId = userId;
        Code = code;
    }
}
