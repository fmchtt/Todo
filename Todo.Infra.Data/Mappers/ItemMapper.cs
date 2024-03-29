﻿using AutoMapper;
using Todo.Application.Results;
using Todo.Domain.Entities;

namespace Todo.Infra.Data.Mappers;

public class ItemMapper : Profile
{
    public ItemMapper()
    {
        CreateMap<TodoItem, ResumedItemResult>();
        CreateMap<TodoItem, ExpandedItemResult>();
    }
}