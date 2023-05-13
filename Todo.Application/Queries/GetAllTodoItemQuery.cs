using System.Text.Json.Serialization;
using MediatR;
using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Application.Queries;

public class GetAllTodoItemQuery : IRequest<PaginatedResult<TodoItem>>
{
    public int Page { get; set; }
    [JsonIgnore] public User User { get; set; }

    public GetAllTodoItemQuery(User user, int page)
    {
        User = user;
        Page = page;
    }
}