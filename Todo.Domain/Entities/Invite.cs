namespace Todo.Domain.Entities;

public class Invite : Entity
{
    public string Email { get; set; }
    public Guid BoardId { get; set; }
    public virtual Board Board { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    #pragma warning disable CS8618
    public Invite(string email, Guid boardId)
    {
        Email = email;
        BoardId = boardId;
    }
}
