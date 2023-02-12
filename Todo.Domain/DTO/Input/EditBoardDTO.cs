using Todo.Domain.Repositories;

namespace Todo.Domain.DTO.Input;

public class EditBoardDTO
{
    public string? Name { get; set; }

    public EditBoardDTO(string name)
    {
        Name = name;
    }
}
