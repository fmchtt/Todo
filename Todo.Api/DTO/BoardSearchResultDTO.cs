namespace Todo.Api.DTO;

public class BoardSearchResultDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public BoardSearchResultDTO(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
