﻿using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Results;

namespace Todo.Infra.Data.Mappers;

public class ItemMapper : Profile
{
    public ItemMapper()
    {
        CreateMap<TodoItem, ResumedItemResult>();
        CreateMap<TodoItem, ExpandedItemResult>();
    }
}