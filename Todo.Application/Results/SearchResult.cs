namespace Todo.Application.Results;

public class SearchResult
{
    public List<TodoItemSearchResult> Itens { get; set; } = new List<TodoItemSearchResult>();
    public List<BoardSearchResult> Boards { get; set; } = new List<BoardSearchResult>();

    public SearchResult(List<TodoItemSearchResult> itens, List<BoardSearchResult> boards)
    {
        Itens = itens;
        Boards = boards;
    }

    public SearchResult() { }
}
