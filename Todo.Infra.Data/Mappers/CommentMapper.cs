using AutoMapper;
using Todo.Application.Results;
using Todo.Domain.Entities;

namespace Todo.Infra.Data.Mappers;

public class CommentMapper : Profile
{
    public CommentMapper()
    {
        CreateMap<Comment, CommentResult>();
    }
}