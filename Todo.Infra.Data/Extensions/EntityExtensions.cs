namespace Todo.Infra.Data.Extensions;

public static class EntityExtensions
{
    public static IQueryable<T> GetPage<T>(this IQueryable<T> query, int page, int pageSize)
    {
        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}