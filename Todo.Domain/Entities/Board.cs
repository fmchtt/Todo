namespace Todo.Domain.Entities;

public class Board : Entity
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public List<TodoItem> Itens { get; set; } = new List<TodoItem>();
    public List<Column> Columns { get; set; } = new List<Column>();
    public List<User> Participants { get; set; } = new List<User>();

    #pragma warning disable CS8618
    public Board(string name, Guid ownerId)
    {
        Name = name;
        OwnerId = ownerId;
    }

    public int GetTodoItemCount()
    {
        return Itens.Count;
    }
}
