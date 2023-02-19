using Todo.Domain.Entities;

namespace Todo.Domain.DTO.Output;

public class BoardResultDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ItemCount { get; set; }
    public int DoneItemCount { get; set; }

    public BoardResultDTO(Board board)
    {
        Id = board.Id;
        Name = board.Name;
        Description = board.Description;
        ItemCount = board.Itens.Count;
        DoneItemCount = board.Itens.Where(x => x.Done == true).Count();
    }
}
