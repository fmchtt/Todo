namespace Todo.Domain.DTO.Input;

public class CreateBoardDTO
{
    public string Name { get; set; }

    public CreateBoardDTO(string name)
    {
        Name = name;
    }
}
