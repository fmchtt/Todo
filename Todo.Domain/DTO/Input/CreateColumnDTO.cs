namespace Todo.Domain.DTO.Input;

public class CreateColumnDTO
{
    public string Name { get; set; }
    public Guid BoardId { get; set; }

    public CreateColumnDTO(string name, Guid boardId)
    {
        Name = name;
        BoardId = boardId;
    }
}
