using Todo.Domain.Entities;

namespace Todo.Domain.DTO.Output;

public class ExpandedColumnDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ItemCount { get; set; }
    public List<TodoItemResultDTO> Itens { get; set; } = new List<TodoItemResultDTO>();

    public ExpandedColumnDTO(Column column)
    {
        Id = column.Id;
        Name = column.Name;
        ItemCount = column.Itens.Count;

        foreach (var item in column.Itens)
        {
            Itens.Add(new TodoItemResultDTO(item));
        }
    }
}
