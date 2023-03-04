namespace Todo.Domain.Results;

public class PaginatedResult<T>
{
    public int PageCount { get; set; }
    public ICollection<T> Results { get; set; }

    public PaginatedResult(ICollection<T> results, int pageCount)
    {
        Results = results;
        PageCount = pageCount;
    }
}
