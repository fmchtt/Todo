﻿using MediatR;
using Todo.Application.Commands.ItemCommands;
using Todo.Application.Exceptions;
using Todo.Application.Queries;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Application.Handlers;

public class ItemHandler : IRequestHandler<CreateItemCommand, TodoItem>, IRequestHandler<EditItemCommand, TodoItem>,
    IRequestHandler<DeleteItemCommand, string>, IRequestHandler<ChangeItemColumnCommand, TodoItem>,
    IRequestHandler<MarkCommand, TodoItem>, IRequestHandler<GetAllTodoItemQuery, PaginatedResult<TodoItem>>, IRequestHandler<GetItemByIdQuery, TodoItem>
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
        var item = await _itemRepository.GetById(command.ItemId);
        if (item == null)
        {
            throw new NotFoundException("Tarefa não encontrada!");
        }

        if (item.Board != null && !item.UserCanEdit(command.User.Id))
        {
            throw new PermissionException("Sem permissão para alterar a Tarefa!");
        }

        if (command.Title != null)
        {
            item.Title = command.Title;
        }

        if (command.Description != null)
        {
            item.Description = command.Description;
        }

        if (command.Priority.HasValue)
        {
            item.Priority = command.Priority.Value;
        }

        item.UpdatedDate = DateTime.UtcNow;
        await _itemRepository.Update(item);

        return item;
    }

    public async Task<string> Handle(DeleteItemCommand command, CancellationToken cancellationToken)
    {
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
        TodoItemFilters filters = new TodoItemFilters();
        if (query.BoardId != null)
        {
            filters.BoardId = query.BoardId;
        }

        if (query.Done != null)
        {
            filters.Done = query.Done;
        }

        var todos = await _itemRepository.GetAll(query.User.Id, query.Page > 1 ? query.Page : 1, filters);

        return todos;
    }

    public async Task<TodoItem> Handle(GetItemByIdQuery query, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetById(query.ItemId);
        if (item == null || item.Creator.Id != query.Actor.Id)
        {
            throw new NotFoundException("Tarefa não encontrada");
        }

        return item;
    }
}