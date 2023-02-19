using Todo.Domain.Entities;

namespace Todo.Domain.DTO.Output;

public class TodoItemResultDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public UserResumedResultDTO? Creator { get; set; }
    public List<string>? Files { get; set; }
    public bool Done { get; set; }

    public TodoItemResultDTO(TodoItem todoItem) {
        Id = todoItem.Id;
        Title = todoItem.Title;
        Description = todoItem.Description;
        CreatedDate = todoItem.CreatedDate;
        UpdatedDate = todoItem.UpdatedDate;
        Files = todoItem.Files;
        Done = todoItem.Done;

        Creator = todoItem.Creator != null ? new UserResumedResultDTO(todoItem.Creator) : null;
    }
}
