using AutoMapper;
using Todo.Application.Results;
using Todo.Domain.Entities;

namespace Todo.Infra.Data.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, ResumedUserResult>();
    }
}