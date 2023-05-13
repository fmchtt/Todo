using MediatR;
using Todo.Application.Queries;
using Todo.Application.Results;
using Todo.Domain.Repositories;

namespace Todo.Application.Handlers;

public class SearchHandler : IRequestHandler<SearchQuery, SearchResult>
{
    private readonly IBoardRepository _boardRepository;
    private readonly ITodoItemRepository _itemRepository;

    public SearchHandler(IBoardRepository boardRepository, ITodoItemRepository itemRepository)
    {
        _boardRepository = boardRepository;
        _itemRepository = itemRepository;
    }

    public async Task<SearchResult> Handle(SearchQuery query, CancellationToken cancellationToken)
    {
        var boards = await _boardRepository.GetAllByName(query.Search, query.User.Id);
        var itens = await _itemRepository.GetAllByTitle(query.Search, query.User.Id);

        var result = new SearchResult();
        foreach (var board in boards)
        {
            result.Boards.Add(new BoardSearchResult(board.Id, board.Name));
        }
        foreach (var item in itens)
        {
            result.Itens.Add(new TodoItemSearchResult(item.Id, item.Title));
        }

        return result;
    }
}