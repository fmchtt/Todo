using Todo.Domain.Entities;

namespace Todo.Domain.DTO.Output;

public class ExpandedBoardDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ExpandedColumnDTO> Columns { get; set; } = new List<ExpandedColumnDTO>();
    public List<UserResumedResultDTO> Participants { get; set; } = new List<UserResumedResultDTO>();
    public Guid Owner { get; set; }
    public int ItemCount { get; set; }
    public int DoneItemCount { get; set; }

    public ExpandedBoardDTO(Board board)
    {
        Id = board.Id;
        Name = board.Name;
        Description = board.Description;
        ItemCount = board.Itens.Count;
        DoneItemCount = board.Itens.Where(x => x.Done == true).Count();
        Owner = board.OwnerId;

        foreach (var column in board.Columns.OrderBy(x => x.Order).ToList())
        {
            Columns.Add(new ExpandedColumnDTO(column));
        }

        foreach (var participant in board.Participants)
        {
            Participants.Add(new UserResumedResultDTO(participant));
        }
    }
}
