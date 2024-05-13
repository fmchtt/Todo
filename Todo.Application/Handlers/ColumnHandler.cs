using MediatR;
using Todo.Application.Commands.ColumnCommands;
using Todo.Application.Exceptions;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Application.Handlers;

public class ColumnHandler : IRequestHandler<CreateColumnCommand, Column>, IRequestHandler<EditColumnCommand, Column>,
    IRequestHandler<DeleteColumnCommand, string>
{
    private readonly IBoardRepository _boardRepository;
    private readonly IColumnRepository _columnRepository;

    public ColumnHandler(IBoardRepository boardRepository, IColumnRepository columnRepository)
    {
        _boardRepository = boardRepository;
        _columnRepository = columnRepository;
    }

    public async Task<Column> Handle(CreateColumnCommand command, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetById(command.BoardId);
        if (board == null)
        {
            throw new NotFoundException("Quadro não encontrado!");
        }

        if (!board.UserCanEdit(command.User.Id))
        {
            throw new PermissionException("Sem permissão para alterar o quadro!");
        }

        var order = await _columnRepository.GetMaxOrder(command.BoardId);

        var column = new Column(command.Name, command.BoardId, order, command.Type);
        await _columnRepository.Create(column);

        return column;
    }

    public async Task<Column> Handle(EditColumnCommand command, CancellationToken cancellationToken)
    {
        var column = await _columnRepository.GetById(command.ColumnId);
        if (column == null)
        {
            throw new NotFoundException("Coluna não encontrada!");
        }

        if (!column.Board.UserCanEdit(command.User.Id))
        {
            throw new PermissionException("Sem permissão para apagar a coluna!");
        }

        if (command.Name != null && column.Name != command.Name)
        {
            column.Name = command.Name;
        }

        if (command.Order != null && column.Order != command.Order)
        {
            column.Order = command.Order.Value;
            await _columnRepository.ColumnReorder(column.BoardId, column.Id, command.Order.Value);
        }

        if (command.Type != null && column.Type != command.Type)
        {
            column.Type = column.Type;
        }

        await _columnRepository.Update(column);

        return column;
    }

    public async Task<string> Handle(DeleteColumnCommand command, CancellationToken cancellationToken)
    {
        var column = await _columnRepository.GetById(command.ColumnId);
        if (column == null)
        {
            throw new NotFoundException("Coluna não encontrada!");
        }

        if (!column.Board.UserCanEdit(command.User.Id))
        {
            throw new PermissionException("Sem permissão para apagar a coluna!");
        }

        await _columnRepository.Delete(column);
        await _columnRepository.ColumnReorder(column.BoardId);

        return "Coluna deletada com sucesso!";
    }
}