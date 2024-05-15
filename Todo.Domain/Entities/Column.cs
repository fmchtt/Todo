namespace Todo.Domain.Entities;

public class Column : Entity
{
    public string Name { get; set; }
    public int Order { get; set; }
    public Guid BoardId { get; set; }
    public EColumnType Type { get; set; }

    public virtual Board Board { get; set; } = null!;
    public virtual List<TodoItem> Itens { get; set; } = [];

    public Column(string name, Guid boardId, int order, EColumnType type)
    {
        Name = name;
        Order = order;
        BoardId = boardId;
        Type = type;
    }

    public int GetTodoItemCount()
    {
        return Itens.Count;
    }
}
