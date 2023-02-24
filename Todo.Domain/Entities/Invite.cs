namespace Todo.Domain.Entities;

public class Invite : Entity
{
    public string Email { get; set; }
    public Guid BoardId { get; set; }
    public Board Board { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public Invite(string email, Guid boardId)
    {
        Email = email;
        BoardId = boardId;
    }
}
