using Todo.Domain.Entities;

namespace Todo.Domain.DTO.Output;

public class BoardResultDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public BoardResultDTO(Board board)
    {
        Id = board.Id;
        Name = board.Name;
    }
}
