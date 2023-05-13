using AutoMapper;
using Todo.Application.Results;
using Todo.Domain.Entities;

namespace Todo.Infra.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, ResumedUserResult>();
    }
}