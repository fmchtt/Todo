namespace Todo.Domain.Entities;

public class TodoItem : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool Done { get; set; }
    public EPriority Priority { get; set; }
    public List<string> Files { get; set; } = new List<string>();
    public Guid? BoardId { get; set; }
    public virtual Board? Board { get; set; }
    public Guid? ColumnId { get; set; }
    public virtual Column? Column { get; set; }
    public Guid CreatorId { get; set; }
    public virtual User Creator { get; set; }

    #pragma warning disable CS8618
    public TodoItem(string title, string description, DateTime createdDate, DateTime updatedDate, Guid? boardId, Guid creatorId, bool done, EPriority priority, Guid? columnId)
    {
        Title = title;
        Description = description;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        BoardId = boardId;
        Done = done;
        CreatorId = creatorId;
        ColumnId = columnId;
        Priority = priority;
    }

    #pragma warning disable CS8618
    public TodoItem(string title, string description, Guid? boardId, Guid creatorId, bool done, EPriority priority, Guid? columnId)
    {
        Title = title;
        Description = description;
        BoardId = boardId;
        CreatedDate = DateTime.UtcNow;
        UpdatedDate = DateTime.UtcNow;
        Done = done;
        CreatorId = creatorId;
        ColumnId = columnId;
        Priority = priority;
    }

    public void MarkAsDone()
    {
        UpdatedDate = DateTime.UtcNow;
        Done = true;
    }

    public void MarkAsUndone()
    {
        UpdatedDate = DateTime.UtcNow;
        Done = false;
    }

    public void ChangeColumn(Column column)
    {
        UpdatedDate = DateTime.UtcNow;
        ColumnId = column.Id;
        Column = column;
    }
}
