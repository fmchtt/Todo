namespace Todo.Domain.Entities;

public class Comment : Entity
{
    public Guid AuthorId { get; set; }
    public virtual User Author { get; set; }
    public string Text { get; set; }
    public DateTime CreationTimeStamp { get; set; }
    public DateTime UpdateTimeStamp { get; set; }
    public Guid ItemId { get; set; }
    public virtual TodoItem Item { get; set; }
    
    public Comment(Guid authorId, Guid itemId, string text)
    {
        AuthorId = authorId;
        ItemId = itemId;
        Text = text;
        CreationTimeStamp = DateTime.UtcNow;
        UpdateTimeStamp = DateTime.UtcNow;
    }
}