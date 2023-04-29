using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Infra.Mappers;

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