namespace Todo.Domain.Entities;

public class Board : Entity
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
    public List<TodoItem>? Tasks { get; set; }
    public List<User> Participants { get; set; }

    public Board(string name, Guid ownerId)
    {
        Name = name;
        OwnerId = ownerId;

        Participants ??= new List<User>();
    }
}
