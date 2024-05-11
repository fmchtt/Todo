using MediatR;
using System.Text.Json.Serialization;
using Todo.Domain.Entities;

namespace Todo.Application.Queries;

public class GetItemByIdQuery : IRequest<TodoItem>
{
    public Guid ItemId { get; set; }

    [JsonIgnore]
    public User Actor { get; set; }

    public GetItemByIdQuery(Guid itemId, User actor)
    {
        Actor = actor;
        ItemId = itemId;
    }
}
