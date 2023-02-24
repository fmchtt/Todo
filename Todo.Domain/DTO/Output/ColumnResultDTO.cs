using Todo.Domain.Entities;

namespace Todo.Domain.DTO.Output;

public class ColumnResultDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public List<TodoItemResultDTO> Itens { get; set; } = new List<TodoItemResultDTO>();

    public ColumnResultDTO(Column column)
    {
        Id = column.Id;
        Name = column.Name;
        Order = column.Order;

        foreach (var item in column.Itens)
        {
            Itens.Add(new TodoItemResultDTO(item));
        }
    }
}
