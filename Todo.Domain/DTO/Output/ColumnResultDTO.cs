using Todo.Domain.Entities;

namespace Todo.Domain.DTO.Output;

public class ColumnResultDTO
{
    public string Name { get; set; }
    public List<TodoItemResultDTO> Itens { get; set; } = new List<TodoItemResultDTO>();

    public ColumnResultDTO(Column column)
    {
        Name = column.Name;
        foreach (var item in column.Itens)
        {
            Itens.Add(new TodoItemResultDTO(item));
        }
    }
}
