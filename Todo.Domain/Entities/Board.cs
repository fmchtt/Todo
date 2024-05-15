namespace Todo.Domain.Entities;

public class Board : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid OwnerId { get; set; }

    public virtual User Owner { get; set; } = null!;
    public virtual List<TodoItem> Itens { get; set; } = [];
    public virtual List<Column> Columns { get; set; } = [];
    public virtual List<User> Participants { get; set; } = [];

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
