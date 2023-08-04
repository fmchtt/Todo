using MediatR;
using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Application.Queries;

public class GetAllCommentsQuery : IRequest<PaginatedResult<Comment>>
{
    public int Page { get; set; } = 1;
    public User User { get; set; }
    public Guid ItemId { get; set; }

    public GetAllCommentsQuery(User user, Guid itemId)
    {
        User = user;
        ItemId = itemId;
    }
    
    public GetAllCommentsQuery(User user, Guid itemId, int page)
    {
        Page = page;
        User = user;
        ItemId = itemId;
    }
}