using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Tests.Repositories;

public class FakeBoardRepository : IBoardRepository
{
    public static User OtherUser = new("Outro Usuario", "other@gmail.com", "senhadooutro", null) { Id = Guid.Parse("441bcc07-8357-4d68-971b-47b2279cd94a") };
    public static User testUser = new("User Teste", "teste@teste.com", "Senha1234@", null) { Id = Guid.Parse("bf0caef8-6f82-4537-8ce6-9c077767860b") };

    public List<Board> Boards = new()
    {
        new Board("Teste", String.Empty, OtherUser.Id) { Participants = new List<User>() { OtherUser } },
        new Board("Teste 2", String.Empty, Guid.Parse("bf0caef8-6f82-4537-8ce6-9c077767860b")) { Id = Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"), Participants = new List<User>() { testUser }, Owner = testUser },
    };

    public void Create(Board board)
    {
        Boards.Add(board);
    }

    public void Delete(Board board)
    {
        Boards.Remove(board);
    }

    public List<Board> GetAll(Guid ownerId, int page)
    {
        return Boards;
    }

    public List<Board> GetAllByName(string name, Guid ownerId)
    {
        return Boards.FindAll(x => x.Name == name && x.OwnerId == ownerId);
    }

    public Board? GetById(Guid id)
    {
        return Boards.Find(x => x.Id == id);
    }

    public void Update(Board board)
    {
        var index = Boards.FindIndex(x => x.Id == board.Id);
        Boards[index] = board;
    }
}
