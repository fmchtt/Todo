namespace Todo.Domain.Entities;

public class Board : Entity
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
    public List<TodoItem> Itens { get; set; }
    public List<Column> Columns { get; set; }
    public List<User> Participants { get; set; }

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
