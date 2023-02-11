﻿using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class CreateColumnUseCase
{
    private readonly IColumnRepository _columnRepository;
    private readonly IBoardRepository _boardRepository;

    public CreateColumnUseCase(IColumnRepository columnRepository, IBoardRepository boardRepository)
    {
        _columnRepository = columnRepository;
        _boardRepository = boardRepository;
    }

    public ResultDTO<ColumnResultDTO> Handle(CreateColumnDTO data, Guid boardId, User user)
    {
        var board = _boardRepository.GetById(boardId);
        if (board == null)
        {
            return new ResultDTO<ColumnResultDTO>(400, "Quadro não encontrado!");
        }

        if (board.Participants.Find(x => x.Id == user.Id) == null)
        {
            return new ResultDTO<ColumnResultDTO>(401, "Sem permissão");
        }

        var column = new Column(data.Name, boardId);
        _columnRepository.Create(column);

        return new ResultDTO<ColumnResultDTO>(200, "Coluna criada com sucesso!", new ColumnResultDTO(column));
    }
}
