using AutoMapper;
using Todo.Application.Results;
using Todo.Domain.Entities;

namespace Todo.Infra.Data.Mappers;

public class ColumnMapper : Profile
{
    public ColumnMapper()
    {
        CreateMap<Column, ResumedColumnResult>();
        CreateMap<Column, ExpandedColumnResult>()
            .ForMember(
                dest => dest.ItemCount,
                opts => opts.MapFrom(src => src.Itens.Count)
            )
            ;
    }
}