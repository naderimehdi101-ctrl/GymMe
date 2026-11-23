
namespace GymMe.Helpers;

public static class CacheTags
{
    public const string Plans = "plans";
    public const string Members = "members";
    public const string Memberships = "memberships";
}

public static class OutputCacheStoreExtensions
{
    public static async ValueTask EvictByTagsAsync(
        this IOutputCacheStore cache,
        CancellationToken ct,
        params string[] tags)
    {
        foreach (var tag in tags)
        {
            await cache.EvictByTagAsync(tag, ct);
        }
    }
}