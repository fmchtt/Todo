namespace Todo.Domain.DTO.Input;

public class CreateColumnDTO
{
    public string Name { get; set; }

    public CreateColumnDTO(string name)
    {
        Name = name;
    }
}
