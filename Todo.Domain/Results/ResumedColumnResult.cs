using Todo.Domain.Entities;

namespace Todo.Domain.Results;

public class ResumedColumnResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public List<ItemResult> Itens { get; set; } = new List<ItemResult>();

    public ResumedColumnResult(Column column)
    {
        Id = column.Id;
        Name = column.Name;
        Order = column.Order;

        foreach (var item in column.Itens)
        {
            Itens.Add(new ItemResult(item));
        }
    }
}
