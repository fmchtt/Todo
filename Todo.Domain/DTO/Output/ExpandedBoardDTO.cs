using Todo.Domain.Entities;

namespace Todo.Domain.DTO.Output;

public class ExpandedBoardDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ExpandedColumnDTO> Columns { get; set; } = new List<ExpandedColumnDTO>();

    public ExpandedBoardDTO(Board board)
    {
        Id = board.Id;
        Name = board.Name;

        foreach (var column in board.Columns)
        {
            Columns.Add(new ExpandedColumnDTO(column));
        }
    }
}
