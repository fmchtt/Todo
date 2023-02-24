namespace Todo.Domain.Entities;

public class Column : Entity
{
    public string Name { get; set; }
    public int Order { get; set; }
    public Guid BoardId { get; set; }
    public virtual Board Board { get; set; }
    public virtual List<TodoItem> Itens { get; set; } = new List<TodoItem>(); 

    #pragma warning disable CS8618
    public Column(string name, Guid boardId, int order)
    {
        Name = name;
        Order = order;
        BoardId = boardId;
    }

    public int GetTodoItemCount()
    {
        return Itens.Count;
    }
}
