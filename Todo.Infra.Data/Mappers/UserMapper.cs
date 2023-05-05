using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Infra.Data.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, ResumedUserResult>();
    }
}