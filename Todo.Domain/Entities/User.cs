namespace Todo.Domain.Entities;

public class User : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? AvatarUrl { get; set; }
    public virtual List<Board> Boards { get; set; } = new List<Board>();
    public virtual List<Board> OwnedBoards { get; set; } = new List<Board>();
    public virtual List<TodoItem> Itens { get; set; } = new List<TodoItem>();

    public User(string name, string email, string password, string? avatarUrl)
    {
        Name = name;
        Email = email;
        Password = password;
        AvatarUrl = avatarUrl;
    }

    public User()
    {
        Id = Guid.Empty;
        Name = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
    }
}
