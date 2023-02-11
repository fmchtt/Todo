namespace Todo.Api.DTO;

public class TodoItemSearchResultDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public TodoItemSearchResultDTO(Guid id, string title)
    {
        Id = id;
        Title = title;
    }
}
