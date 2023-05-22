using MediatR;
using Todo.Application.Commands.ItemCommands;
using Todo.Application.Exceptions;
using Todo.Application.Queries;
using Todo.Application.Results;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Application.Handlers;

public class ItemHandler : IRequestHandler<CreateItemCommand, TodoItem>, IRequestHandler<EditItemCommand, TodoItem>,
    IRequestHandler<DeleteItemCommand, string>, IRequestHandler<ChangeItemColumnCommand, TodoItem>,
    IRequestHandler<MarkCommand, TodoItem>, IRequestHandler<GetAllTodoItemQuery, PaginatedResult<TodoItem>>
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

    public async Task<TodoItem> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        if (command.BoardId.HasValue)
        {
            var board = await _boardRepository.GetById(command.BoardId.Value);

            if (board == null || !board.UserCanEdit(command.User.Id))
            {
                throw new PermissionException(
                    "Usuário não autorizado a adicionar itens nesse quadro!");
            }
        }

        if (command.ColumnId.HasValue)
        {
            var column = await _columnRepository.GetById(command.ColumnId.Value);

            if (column == null || !column.Board.UserCanEdit(command.User.Id))
            {
                throw new PermissionException(
                    "Usuário não autorizado a adicionar itens nessa coluna!");
            }
        }

        var todoItem = new TodoItem(
            command.Title,
            command.Description,
            command.BoardId,
            command.User.Id,
            false,
            command.Priority,
            command.ColumnId
        );

        await _itemRepository.Create(todoItem);

        return todoItem;
    }

    public async Task<TodoItem> Handle(EditItemCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var item = await _itemRepository.GetById(command.ItemId);
        if (item == null)
        {
            throw new NotFoundException("Tarefa não encontrada!");
        }

        if (item.Board != null && !item.UserCanEdit(command.User.Id))
        {
            throw new PermissionException("Sem permissão para alterar a Tarefa!");
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
        await _itemRepository.Update(item);

        return item;
    }

    public async Task<string> Handle(DeleteItemCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var item = await _itemRepository.GetById(command.ItemId);
        if (item == null)
        {
            throw new NotFoundException("Tarefa não encontrada!");
        }

        if (item.Board != null && !item.UserCanEdit(command.User.Id))
        {
            throw new PermissionException("Sem permissão para apagar a tarefa!");
        }

        await _itemRepository.Delete(item);

        return "Tarefa apagada com sucesso!";
    }

    public async Task<TodoItem> Handle(ChangeItemColumnCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var todo = await _itemRepository.GetById(command.ItemId);
        var column = await _columnRepository.GetById(command.ColumnId);

        if (todo == null || column == null)
        {
            throw new NotFoundException("Tarefa ou coluna não encontrados!");
        }

        if (!column.Board.UserCanEdit(command.User.Id))
        {
            throw new NotFoundException("Sem permissão para alterar a tarefa!");
        }

        todo.ChangeColumn(column);
        await _itemRepository.Update(todo);

        return todo;
    }

    public async Task<TodoItem> Handle(MarkCommand command, CancellationToken cancellationToken)
    {
        var validation = command.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException("Comando inválido",
                validation.Errors.Select(error => new ErrorResult(error)).ToList());
        }

        var item = await _itemRepository.GetById(command.ItemId);
        if (item == null)
        {
            throw new NotFoundException("Tarefa não encontrada!");
        }

        if (!item.UserCanEdit(command.User.Id))
        {
            throw new PermissionException("Sem permissão para alterar a Tarefa!");
        }

        item.MarkAsDone();
        await _itemRepository.Update(item);

        return item;
    }

    public async Task<PaginatedResult<TodoItem>> Handle(GetAllTodoItemQuery query, CancellationToken cancellationToken)
    {
        var todos = await _itemRepository.GetAll(query.User.Id, query.Page > 1 ? query.Page : 1);

        return todos;
    }
}