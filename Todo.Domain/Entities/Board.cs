namespace Todo.Domain.Entities;

public class Board : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
    public virtual List<TodoItem> Itens { get; set; } = new List<TodoItem>();
    public virtual List<Column> Columns { get; set; } = new List<Column>();
    public virtual List<User> Participants { get; set; } = new List<User>();

    #pragma warning disable CS8618
    public Board(string name, string description, Guid ownerId)
    {
        Name = name;
        Description = description;
        OwnerId = ownerId;
    }

    public bool UserCanEdit(Guid userId)
    {
        return Participants.Find(x => x.Id == userId) != null;
    }

    public int GetTodoItemCount()
    {
        return Itens.Count;
    }
}
