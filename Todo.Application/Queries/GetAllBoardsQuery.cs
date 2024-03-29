﻿using MediatR;
using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Application.Queries;

public class GetAllBoardsQuery : IRequest<PaginatedResult<Board>>
{
    public int Page { get; set; }
    public User User { get; set; }

    public GetAllBoardsQuery(int page, User user)
    {
        Page = page;
        User = user;
    }
}