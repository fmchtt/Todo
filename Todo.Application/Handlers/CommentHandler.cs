using MediatR;
using Todo.Application.Commands.CommentCommands;
using Todo.Application.Exceptions;
using Todo.Application.Queries;
using Todo.Application.Results;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Domain.Results;

namespace Todo.Application.Handlers;

public class CommentHandler : IRequestHandler<CreateCommentCommand, Comment>,
    IRequestHandler<GetAllCommentsQuery, PaginatedResult<Comment>>, IRequestHandler<DeleteCommentCommand, string>,
    IRequestHandler<EditCommentCommand, Comment>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ITodoItemRepository _itemRepository;

    public CommentHandler(
        ICommentRepository commentRepository,
        ITodoItemRepository itemRepository
    )
    {
        _commentRepository = commentRepository;
        _itemRepository = itemRepository;
    }

    public async Task<Comment> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
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
            throw new NotFoundException("Quadro não encontrado!");
        }

        if (item.Board != null && !item.Board.UserCanEdit(command.User.Id))
        {
            throw new PermissionException("Usuário não pertencente ao quadro!");
        }

        var comment = new Comment(command.User.Id, command.ItemId, command.Text);
        await _commentRepository.Create(comment);

        return comment;
    }

    public async Task<PaginatedResult<Comment>> Handle(GetAllCommentsQuery command, CancellationToken cancellationToken)
    {
        if (command.Page < 1)
        {
            command.Page = 1;
        }

        var item = await _itemRepository.GetById(command.ItemId);
        if (item == null)
        {
            throw new NotFoundException("Tarefa não encontrada!");
        }

        if (item.Board != null && !item.Board.UserCanEdit(command.User.Id))
        {
            throw new PermissionException("Usuário não pertence ao quadro!");
        }

        var comments = await _commentRepository.GetAllByItemId(command.ItemId, command.Page);
        return comments;
    }

    public async Task<string> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetById(command.CommentId);
        if (comment == null)
        {
            throw new NotFoundException("Comentário não encontrada!");
        }

        if (!comment.Author.Equals(command.User) ||
            (comment.Item.Board != null && comment.Item.Board.OwnerId != command.User.Id))
        {
            throw new PermissionException("Usuário não pertence ao quadro!");
        }

        await _commentRepository.Delete(comment);
        return "Comentário deletado com sucesso!";
    }

    public async Task<Comment> Handle(EditCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetById(command.CommentId);
        if (comment == null)
        {
            throw new NotFoundException("Comentário não encontrada!");
        }

        if (!comment.Author.Equals(command.User))
        {
            throw new PermissionException("Usuário não é autor do comentário!");
        }

        comment.Text = command.Text;
        comment.UpdateTimeStamp = DateTime.UtcNow;

        await _commentRepository.Update(comment);
        return comment;
    }
}