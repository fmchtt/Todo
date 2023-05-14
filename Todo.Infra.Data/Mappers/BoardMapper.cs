using AutoMapper;
using Todo.Application.Results;
using Todo.Domain.Entities;

namespace Todo.Infra.Data.Mappers;

public class BoardMapper : Profile
{
    public BoardMapper()
    {
        CreateMap<Board, ResumedBoardResult>()
            .ForMember(
                dest => dest.ItemCount,
                opts => opts.MapFrom(src => src.Itens.Count)
            )
            .ForMember(
                dest => dest.DoneItemCount,
                opts => opts.MapFrom(src => src.Itens.Count(x => x.Done == true))
            );
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