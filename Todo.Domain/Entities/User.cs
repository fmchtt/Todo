namespace Todo.Domain.Entities;

public class User : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Board> Boards { get; set; } = new List<Board>();
    public List<Board> OwnedBoards { get; set; } = new List<Board>();
    public List<TodoItem> Itens { get; set; } = new List<TodoItem>();

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}
