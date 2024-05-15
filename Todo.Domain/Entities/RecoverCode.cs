namespace Todo.Domain.Entities;

public class RecoverCode : Entity
{
    public Guid UserId { get; set; }
    public int Code { get; set; }

    public virtual User User { get; set; } = null!;

    public RecoverCode(Guid userId, int code)
    {
        UserId = userId;
        Code = code;
    }
}
