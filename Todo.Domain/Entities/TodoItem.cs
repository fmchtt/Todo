namespace Todo.Domain.Entities;

public class TodoItem : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool Done { get; set; }

    /*
     * Priority são opções
     * 0 - Nenhuma
     * 1 - Muito Baixa
     * 2 - Baixa
     * 3 - Media
     * 4 - Alta
     * 5 - Muito Alta
     */
    public int Priority { get; set; }
    public List<string>? Files { get; set; }
    public Guid? BoardId { get; set; }
    public Board? Board { get; set; }
    public Guid? ColumnId { get; set; }
    public Column? Column { get; set; }
    public Guid CreatorId { get; set; }
    public User? Creator { get; set; }

    public TodoItem(string title, string description, DateTime createdDate, DateTime updatedDate, Guid? boardId, Guid creatorId, bool done, int priority, List<string>? files, Guid? columnId)
    {
        Title = title;
        Description = description;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        Files = files;
        BoardId = boardId;
        Done = done;
        CreatorId = creatorId;
        ColumnId = columnId;
        Priority = priority;
    }

    public TodoItem(string title, string description, Guid? boardId, Guid creatorId, bool done, int priority, List<string>? files, Guid? columnId)
    {
        Title = title;
        Description = description;
        Files = files;
        BoardId = boardId;
        CreatedDate = DateTime.Now;
        UpdatedDate = DateTime.Now;
        Done = done;
        CreatorId = creatorId;
        ColumnId = columnId;
        Priority = priority;
    }

    public void MarkAsDone()
    {
        UpdatedDate = DateTime.Now;
        Done = true;
    }

    public void MarkAsUndone()
    {
        UpdatedDate = DateTime.Now;
        Done = false;
    }

    public void ChangeColumn(Column column)
    {
        UpdatedDate = DateTime.Now;
        ColumnId = column.Id;
        Column = column;
    }
}
