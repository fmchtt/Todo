using System.Text.Json.Serialization;
using MediatR;
using Todo.Application.Results;
using Todo.Domain.Entities;

namespace Todo.Application.Queries;

public class SearchQuery : IRequest<SearchResult>
{
    public string Search { get; set; }
    [JsonIgnore] public User User { get; set; }

    public SearchQuery(string search, User user)
    {
        User = user;
        Search = search;
    }
}