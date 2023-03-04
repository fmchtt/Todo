namespace Todo.Domain.Results;

public class PaginatedDTO<T>
{
    public int PageCount { get; set; }
    public ICollection<T> Results { get; set; }

    public PaginatedDTO(ICollection<T> results, int pageCount)
    {
        Results = results;
        PageCount = pageCount;
    }
}
