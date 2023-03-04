using Todo.Domain.Commands.ColumnCommands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers;

public class ColumnHandler : IHandler<CreateColumnCommand, Column>, IHandler<EditColumnCommand, Column>, IHandler<DeleteColumnCommand>
{
    private readonly IBoardRepository _boardRepository;
    private readonly IColumnRepository _columnRepository;
    
    public ColumnHandler(IBoardRepository boardRepository, IColumnRepository columnRepository)
    {
        _boardRepository = boardRepository;
        _columnRepository = columnRepository;
    }
    
    public CommandResult<Column> Handle(CreateColumnCommand command, User user)
    {
        var board = _boardRepository.GetById(command.BoardId);
        if (board == null)
        {
            return new CommandResult<Column>(Code.Invalid, "Quadro não encontrado!");
        }
        
        if (!board.UserCanEdit(user.Id))
        {
            return new CommandResult<Column>(Code.Unauthorized, "Sem permissão para alterar o quadro!");
        }

        var order = _columnRepository.GetMaxOrder(command.BoardId);

        var column = new Column(command.Name, command.BoardId, order);
        _columnRepository.Create(column);

        return new CommandResult<Column>(Code.Created, "Coluna criada com sucesso!", column);
    }

    public CommandResult<Column> Handle(EditColumnCommand command, User user)
    {
        var column = _columnRepository.GetById(command.ColumnId);
        if (column == null)
        {
            return new CommandResult<Column>(Code.NotFound, "Coluna não encontrada!");
        }

        if (!column.Board.UserCanEdit(user.Id))
        {
            return new CommandResult<Column>(Code.NotFound, "Sem permissão para apagar a coluna!");
        }

        if (command.Name != null && column.Name != command.Name)
        {
            column.Name = command.Name;
        }

        if (command.Order != null && column.Order != command.Order)
        {
            column.Order = command.Order.Value;
            _columnRepository.ColumnReorder(column.BoardId, column.Id, command.Order.Value);
        }

        _columnRepository.Update(column);

        return new CommandResult<Column>(Code.Ok, "Coluna alterada com sucesso!", column);
    }

    public CommandResult Handle(DeleteColumnCommand command, User user)
    {
        var column = _columnRepository.GetById(command.ColumnId);
        if (column == null)
        {
            return new CommandResult(Code.NotFound, "Coluna não encontrada!");
        }

        if (!column.Board.UserCanEdit(user.Id))
        {
            return new CommandResult(Code.Unauthorized, "Sem permissão para apagar a coluna!");
        }

        _columnRepository.Delete(column);
        _columnRepository.ColumnReorder(column.BoardId);

        return new CommandResult(Code.Ok, "Coluna deletada com sucesso!");
    }
}