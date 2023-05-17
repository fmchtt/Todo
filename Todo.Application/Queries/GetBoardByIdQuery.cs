using System.Text.Json.Serialization;
using MediatR;
using Todo.Domain.Entities;

namespace Todo.Application.Queries;

public class GetBoardByIdQuery : IRequest<Board>
{
    public Guid BoardId { get; set; }
    [JsonIgnore] public User User { get; set; }

    public GetBoardByIdQuery(Guid boardId, User user)
    {
        BoardId = boardId;
        User = user;
    }
}