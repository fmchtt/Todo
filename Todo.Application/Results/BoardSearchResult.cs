namespace Todo.Application.Results;

public class BoardSearchResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public BoardSearchResult(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
