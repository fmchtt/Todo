namespace Todo.Domain.Entities;

public class TodoItem : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public List<string>? Files { get; set; }
    public Guid BoardId { get; set; }
    public Board? Board { get; set; }
    public Guid CreatorId { get; set; }
    public User? Creator { get; set; }
    public bool Done { get; set; }

    public TodoItem(string title, string description, DateTime createdDate, DateTime updatedDate, Guid boardId, Guid creatorId, bool done, List<string>? files)
    {
        Title = title;
        Description = description;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        Files = files;
        BoardId = boardId;
        Done = done;
        CreatorId = creatorId;
    }

    public TodoItem(string title, string description, Guid boardId, Guid creatorId, bool done, List<string>? files)
    {
        Title = title;
        Description = description;
        Files = files;
        BoardId = boardId;
        CreatedDate = DateTime.Now; 
        UpdatedDate = DateTime.Now;
        Done = done;
        CreatorId = creatorId;
    }

    public void MarkAsDone()
    {
        Done = true;
    }

    public void MarkAsUndone()
    {
        Done = false;
    }
}
