using Todo.Domain.Entities;

namespace Todo.Domain.DTO.Output;

public class BoardResultDTO
{
    public string Name { get; set; }

    public BoardResultDTO(Board board)
    {
        Name = board.Name;
    }
}
