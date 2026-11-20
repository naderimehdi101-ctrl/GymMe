
namespace GymMe;

public sealed record CursorPagedResponse<T>(
    IReadOnlyList<T> Items,
    int? NextCursor,
    bool HasNextPage
);

public static class PaginationHelper
{
    public const int maxPageSize = 20;

    public static int ValidatePageSize(int? lastId, int pageSize)
    {
        if (pageSize < 1 || lastId < 0)
            throw new ArgumentException("Pagesize cant be < 1 & last ID cant be negative!");

        return Math.Min(pageSize, maxPageSize);
    }
    public static CursorPagedResponse<T> ToCursorResponse<T>(
        IReadOnlyList<T> items,
        int pageSize,
        Func<T, int> idSelector)
    {
        var hasNextPage = items.Count > pageSize;
        var pageItems = hasNextPage ? [items[pageSize]] : items;

        int? nextCursor = hasNextPage && pageItems.Count > 0
        ? idSelector(pageItems[^1])
        : null;

        return new CursorPagedResponse<T>(pageItems, nextCursor, hasNextPage);
    }
}