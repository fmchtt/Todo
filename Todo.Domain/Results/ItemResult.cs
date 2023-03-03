using Todo.Domain.Entities;

namespace Todo.Domain.Results;

public class ItemResult
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public EPriority Priority { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public ResumedUserResult? Creator { get; set; }
    public bool Done { get; set; }

    public ItemResult(TodoItem todoItem) {
        Id = todoItem.Id;
        Title = todoItem.Title;
        Description = todoItem.Description;
        CreatedDate = todoItem.CreatedDate;
        UpdatedDate = todoItem.UpdatedDate;
        Done = todoItem.Done;
        Priority = todoItem.Priority;

        Creator = todoItem.Creator != null ? new ResumedUserResult(todoItem.Creator) : null;
    }
}
