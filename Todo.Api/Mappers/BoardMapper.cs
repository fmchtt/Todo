using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Api.Mappers;

public class BoardMapper : Profile
{
    public BoardMapper()
    {
        CreateMap<Board, ResumedBoardResult>();
        CreateMap<Board, ExpandedBoardResult>()
            .ForMember(
                dest => dest.Owner,
                opts => opts.MapFrom(src => src.Owner.Id)
            )
            .ForMember(
                dest => dest.ItemCount,
                opts => opts.MapFrom(src => src.Itens.Count)
            )
            .ForMember(
                dest => dest.DoneItemCount,
                opts => opts.MapFrom(src => src.Itens.Count(x => x.Done == true))
            )
            ;
    }
}