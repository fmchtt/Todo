using Todo.Domain.Entities;

namespace Todo.Domain.Results;

public class ExpandedBoardResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ExpandedColumnResult> Columns { get; set; } = new List<ExpandedColumnResult>();
    public List<ResumedUserResult> Participants { get; set; } = new List<ResumedUserResult>();
    public Guid Owner { get; set; }
    public int ItemCount { get; set; }
    public int DoneItemCount { get; set; }

    public ExpandedBoardResult(Board board)
    {
        Id = board.Id;
        Name = board.Name;
        Description = board.Description;
        ItemCount = board.Itens.Count;
        DoneItemCount = board.Itens.Count(x => x.Done == true);
        Owner = board.OwnerId;

        foreach (var column in board.Columns.OrderBy(x => x.Order).ToList())
        {
            Columns.Add(new ExpandedColumnResult(column));
        }

        foreach (var participant in board.Participants)
        {
            Participants.Add(new ResumedUserResult(participant));
        }
    }
}
