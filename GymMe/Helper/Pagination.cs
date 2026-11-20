namespace GymMe;

public sealed record CursorPagedResponse<T>(
    IReadOnlyList<T> Items,
    int? NextCursor,
    bool HasNextPage
);

public static class PaginationHelper
{
    public const int maxPageSize = 20;

    public static async Task<CursorPagedResponse<T>> ToCursorPageAsync<T>(
        this IQueryable<T> query,
        int? lastId,
        int pageSize,
        Func<T, int> IdSelector,
        CancellationToken ct = default
        )
    {
        if (lastId < 0 || pageSize < 1)
            throw new ArgumentException("Last ID cannot be negative & Pagesize must be atleast 1");

        pageSize = Math.Min(pageSize, maxPageSize);

        var items = await query.Take(pageSize + 1).ToListAsync(ct);
        
        bool hasNextPage = items.Count > pageSize;
        
        if(hasNextPage)
            items.RemoveAt(items.Count - 1);
            
        int? nextCursor = hasNextPage ? IdSelector(items[^1]) : null;
        
        return new CursorPagedResponse<T>(
            items,
            nextCursor,
            hasNextPage
        );   
    }


}
