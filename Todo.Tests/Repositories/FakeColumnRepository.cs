using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Tests.Repositories;

public class FakeColumnRepository : IColumnRepository
{
    public static User OtherUser = new("Outro Usuario", "other@gmail.com", "senhadooutro", null) { Id = Guid.Parse("441bcc07-8357-4d68-971b-47b2279cd94a") };
    public static User testUser = new("User Teste", "teste@teste.com", "Senha1234@", null) { Id = Guid.Parse("bf0caef8-6f82-4537-8ce6-9c077767860b") };

    public static Board testBoard = new("Teste 2", String.Empty, Guid.Parse("bf0caef8-6f82-4537-8ce6-9c077767860b")) { Id = Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"), Participants = new List<User>() { testUser }, Owner = testUser };
    public static Board otherBoard = new("Teste", String.Empty, OtherUser.Id) { Participants = new List<User>() { OtherUser } };

    public List<Column> Columns = new()
    {
        new Column("Aberto", testBoard.Id, 0) { Board = testBoard, Id = Guid.Parse("c2963d0f-7551-4fd2-b8f4-561714bb0ce6") },
        new Column("Em Andamento", testBoard.Id, 1) { Board = testBoard, Id = Guid.Parse("974a4dcb-3cbe-4064-9476-404462798702") },
        new Column("Fechado", testBoard.Id, 2) { Board = testBoard, Id = Guid.Parse("5d1c65a5-35c3-4ea9-bad2-d40579d171ba") },
    };

    public int GetMaxOrder(Guid boardId)
    {
        return Columns.Max(x => x.Order) + 1;
    }

    public void Create(Column column)
    {
        Columns.Add(column);
    }

    public void ColumnReorder(Guid boardId, Guid columnId, int order)
    {
        for (var i = 0; i < Columns.Count; i++)
        {
            Columns[i].Order = i;
        }
    }

    public void Delete(Column column)
    {
        Columns.Remove(column);
    }

    public Column? GetById(Guid id)
    {
        return Columns.Find(x => x.Id == id);
    }

    public void Update(Column column)
    {
        var index = Columns.FindIndex(x => x.Id == column.Id);
        Columns[index] = column;
    }

    public void ColumnReorder(Guid boardId)
    {
        for (var i = 0; i < Columns.Count; i++)
        {
            Columns[i].Order = i;
        }
    }
}
