﻿namespace Todo.Domain.DTO.Input;

public class EditColumnDTO
{
    public string? Name { get; set; }

    public EditColumnDTO(string? name)
    {
        Name = name;
    }
}