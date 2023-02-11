namespace Todo.Api.DTO;

public class SearchResultDTO
{
    public List<TodoItemSearchResultDTO> Itens { get; set; } = new List<TodoItemSearchResultDTO>();
    public List<BoardSearchResultDTO> Boards { get; set; } = new List<BoardSearchResultDTO>();

    public SearchResultDTO(List<TodoItemSearchResultDTO> itens, List<BoardSearchResultDTO> boards)
    {
        Itens = itens;
        Boards = boards;
    }

    public SearchResultDTO() { }
}
