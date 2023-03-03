using Todo.Domain.Entities;

namespace Todo.Domain.Results;

public class ResumedBoardResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ItemCount { get; set; }
    public int DoneItemCount { get; set; }

    public ResumedBoardResult(Board board)
    {
        Id = board.Id;
        Name = board.Name;
        Description = board.Description;
        ItemCount = board.Itens.Count;
        DoneItemCount = board.Itens.Count(x => x.Done == true);
    }
}
