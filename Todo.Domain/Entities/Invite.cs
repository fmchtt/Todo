namespace Todo.Domain.Entities;

public class Invite : Entity
{
    public string Email { get; set; }
    public Guid BoardId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public virtual Board Board { get; set; } = null!;

    public Invite(string email, Guid boardId)
    {
        Email = email;
        BoardId = boardId;
    }
}
