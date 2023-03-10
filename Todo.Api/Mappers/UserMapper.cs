using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Api.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, ResumedUserResult>();
    }
}