using Todo.Domain.Entities;

namespace Todo.Domain.DTO.Output;

public class ExpandedTodoItemResultDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public EPriority Priority { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public UserResumedResultDTO? Creator { get; set; }
    public BoardResultDTO? Board { get; set; }
    public bool Done { get; set; }

    public ExpandedTodoItemResultDTO(TodoItem todoItem)
    {
        Id = todoItem.Id;
        Title = todoItem.Title;
        Description = todoItem.Description;
        CreatedDate = todoItem.CreatedDate;
        UpdatedDate = todoItem.UpdatedDate;
        Done = todoItem.Done;
        Priority = todoItem.Priority;
        Board = todoItem.Board != null ? new BoardResultDTO(todoItem.Board) : null;
        Creator = todoItem.Creator != null ? new UserResumedResultDTO(todoItem.Creator) : null;
    }
}
