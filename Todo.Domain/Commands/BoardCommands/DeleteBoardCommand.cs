using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.BoardCommands;

public class DeleteBoardCommand : ICommand
{
    public Guid BoardId { get; set; }

    public DeleteBoardCommand(Guid boardId)
    {
        BoardId = boardId;
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}