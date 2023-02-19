namespace Todo.Domain.DTO.Input;

public class CreateBoardDTO
{
    public string Name { get; set; }
    public string Description { get; set; }

    public CreateBoardDTO(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
