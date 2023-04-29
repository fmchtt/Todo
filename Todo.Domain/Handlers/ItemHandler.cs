using Todo.Domain.Commands.ItemCommands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Domain.Handlers;

public class ItemHandler : IHandler<CreateItemCommand, TodoItem>, IHandler<EditItemCommand, TodoItem>,
    IHandler<DeleteItemCommand>, IHandler<ChangeItemColumnCommand, TodoItem>, IHandler<MarkCommand, TodoItem>
{
    private readonly IBoardRepository _boardRepository;
    private readonly IColumnRepository _columnRepository;
    private readonly ITodoItemRepository _itemRepository;

    public ItemHandler(IBoardRepository boardRepository, IColumnRepository columnRepository,
        ITodoItemRepository itemRepository)
    {
        _boardRepository = boardRepository;
        _columnRepository = columnRepository;
        _itemRepository = itemRepository;
    }

    public CommandResult<TodoItem> Handle(CreateItemCommand command, User user)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            return new CommandResult<TodoItem>(Code.Invalid, "Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        if (command.BoardId.HasValue)
        {
            var board = _boardRepository.GetById(command.BoardId.Value);

            if (board == null || !board.UserCanEdit(user.Id))
            {
                return new CommandResult<TodoItem>(Code.Unauthorized,
                    "Usuário não autorizado a adicionar itens nesse quadro!");
            }
        }

        if (command.ColumnId.HasValue)
        {
            var column = _columnRepository.GetById(command.ColumnId.Value);

            if (column == null || !column.Board.UserCanEdit(user.Id))
            {
                return new CommandResult<TodoItem>(Code.Unauthorized,
                    "Usuário não autorizado a adicionar itens nessa coluna!");
            }
        }

        var todoItem = new TodoItem(command.Title, command.Description, command.BoardId, user.Id, false,
            command.Priority, command.ColumnId);
        _itemRepository.Create(todoItem);

        return new CommandResult<TodoItem>(Code.Created, "Item criado com sucesso!", todoItem);
    }

    public CommandResult<TodoItem> Handle(EditItemCommand command, User user)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            return new CommandResult<TodoItem>(Code.Invalid, "Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var item = _itemRepository.GetById(command.ItemId);
        if (item == null)
        {
            return new CommandResult<TodoItem>(Code.NotFound, "Tarefa não encontrada!");
        }

        if (item.Board != null && !item.UserCanEdit(user.Id))
        {
            return new CommandResult<TodoItem>(Code.Unauthorized, "Sem permissão para alterar a Tarefa!");
        }

        foreach (var prop in command.GetType().GetProperties())
        {
            var value = prop.GetValue(command, null);
            if (value != null)
            {
                prop.SetValue(item, value);
            }
        }

        item.UpdatedDate = DateTime.UtcNow;
        _itemRepository.Update(item);

        return new CommandResult<TodoItem>(Code.Ok, "Tarefa editada com sucesso!", item);
    }

    public CommandResult Handle(DeleteItemCommand command, User user)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            return new CommandResult(Code.Invalid, "Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var item = _itemRepository.GetById(command.ItemId);
        if (item == null)
        {
            return new CommandResult(Code.NotFound, "Tarefa não encontrada!");
        }

        if (item.Board != null && !item.UserCanEdit(user.Id))
        {
            return new CommandResult(Code.Unauthorized, "Sem permissão para apagar a tarefa!");
        }

        _itemRepository.Delete(item);

        return new CommandResult(Code.Ok, "Tarefa apagada com sucesso!");
    }

    public CommandResult<TodoItem> Handle(ChangeItemColumnCommand command, User user)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            return new CommandResult<TodoItem>(Code.Invalid, "Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var todo = _itemRepository.GetById(command.ItemId);
        var column = _columnRepository.GetById(command.ColumnId);

        if (todo == null || column == null)
        {
            return new CommandResult<TodoItem>(Code.NotFound, "Tarefa ou coluna não encontrados!");
        }

        if (!column.Board.UserCanEdit(user.Id))
        {
            return new CommandResult<TodoItem>(Code.NotFound, "Sem permissão para alterar a tarefa!");
        }

        todo.ChangeColumn(column);
        _itemRepository.Update(todo);

        return new CommandResult<TodoItem>(Code.Ok, "Tarefa alterar com sucesso!", todo);
    }

    public CommandResult<TodoItem> Handle(MarkCommand command, User user)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            return new CommandResult<TodoItem>(Code.Invalid, "Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var item = _itemRepository.GetById(command.ItemId);
        if (item == null)
        {
            return new CommandResult<TodoItem>(Code.NotFound, "Tarefa não encontrada!");
        }

        if (!item.UserCanEdit(user.Id))
        {
            return new CommandResult<TodoItem>(Code.Unauthorized, "Sem permissão para alterar a Tarefa!");
        }

        item.MarkAsDone();
        _itemRepository.Update(item);

        return new CommandResult<TodoItem>(Code.Ok, "Tarefa atualizada com sucesso!", item);
    }
}