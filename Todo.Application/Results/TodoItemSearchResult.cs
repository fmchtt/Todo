namespace Todo.Application.Results;

public class TodoItemSearchResult
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public TodoItemSearchResult(Guid id, string title)
    {
        Id = id;
        Title = title;
    }
}
