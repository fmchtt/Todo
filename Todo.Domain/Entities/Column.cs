namespace Todo.Domain.Entities;

public class Column : Entity
{
    public string Name { get; set; }
    public Guid BoardId { get; set; }
    public Board? Board { get; set; }
    public List<TodoItem>? Itens { get; set; }

    public Column(string name, Guid boardId)
    {
        Name = name;
        BoardId = boardId;
    }

    public int GetTodoItemCount()
    {
        return Itens != null ? Itens.Count : 0;
    }
}
