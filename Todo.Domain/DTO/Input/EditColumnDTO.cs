namespace Todo.Domain.DTO.Input;

public class EditColumnDTO
{
    public string? Name { get; set; }
    public int? Order { get; set; }

    public EditColumnDTO(string? name, int? order)
    {
        Name = name;
        Order = order;
    }
}
