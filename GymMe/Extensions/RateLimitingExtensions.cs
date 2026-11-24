
using RequiredAttribute = 
System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace GymMe.RateLimiting;

public sealed class PolicySettings
{
    [Range(1, int.MaxValue,
    ErrorMessage = "Permit limit must be atleast 1!")]
    public int PermitLimit { get; init; }
    
    [Range(1, int.MaxValue,
    ErrorMessage = "Window seconds must be atleast 1 sec!")]
    public int WindowSecond { get; init; }
    
    [Range(0, int.MaxValue,
    ErrorMessage = "Queuelimit cannot be negative!")]
    public int QueueLimit { get; init; }
}

public sealed class RateLimitOptions
{
    public const string SectionName = "RateLimiting";
    
    [Required]
    [ValidateObjectMembers]
    public PolicySettings AuthStrict { get; init; } = new()
    {
        PermitLimit = 10,
        WindowSecond = 60,
        QueueLimit = 0
    };

    [Required]
    [ValidateObjectMembers]
    public PolicySettings ApiStandard { get; init; } = new()
    {
        PermitLimit = 120,
        WindowSecond = 60,
        QueueLimit = 0
    };
}

public static class RateLimitingPolicies
{
    public const string AuthStrict = "AuthStrictPolicy";
    public const string ApiStandard = "ApiStandardPolicy";
}

internal static class RateLimitPartitionKey
{
    public static string ForUserOrIp(HttpContext httpContext)
    {
        var userId =
            httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ??
            httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (!string.IsNullOrWhiteSpace(userId))
            return $"user ID: {userId}";

        var ip = httpContext.Connection.RemoteIpAddress;
        return ip is not null ?
        $"IP: {ip}" : $"IP: {IPAddress.None}";
    }
}

public static class RateLimitingExtension
{
    public static IServiceCollection AddGymMeRateLimiting(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionsWithValidateOnStart<RateLimitOptions>()
        .BindConfiguration(RateLimitOptions.SectionName)
        .ValidateDataAnnotations();

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.OnRejected = async (context, cancellationToken) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    var seconds = Math.Max(1, (int)Math.Ceiling(retryAfter.TotalSeconds));

                    context.HttpContext.Response.Headers.RetryAfter = seconds.ToString();
                }

                await context.HttpContext.Response.WriteAsJsonAsync(new
                {
                    error = "Too many requests. Please slow down and try again shortly!"
                }, cancellationToken);

            };

            options.AddPolicy(RateLimitingPolicies.AuthStrict, httpContext =>
            {
                var settings = httpContext.RequestServices.
                    GetRequiredService<IOptions<RateLimitOptions>>()
                    .Value
                    .AuthStrict;

                var partitionKey = RateLimitPartitionKey.ForUserOrIp(httpContext);
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: partitionKey,
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = settings.PermitLimit,
                        Window = TimeSpan.FromSeconds(settings.WindowSecond),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = settings.QueueLimit,
                        AutoReplenishment = true
                    });
            });

            options.AddPolicy(RateLimitingPolicies.ApiStandard, httpContext =>
            {
                var settings = httpContext.RequestServices
                    .GetRequiredService<IOptions<RateLimitOptions>>()
                    .Value.ApiStandard;

                var partitionKey = RateLimitPartitionKey.ForUserOrIp(httpContext);
                return RateLimitPartition.GetSlidingWindowLimiter(
                partitionKey: partitionKey,
                factory: _ => new SlidingWindowRateLimiterOptions
                {
                    PermitLimit = settings.PermitLimit,
                    Window = TimeSpan.FromSeconds(settings.WindowSecond),
                    SegmentsPerWindow = 6,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = settings.QueueLimit,
                    AutoReplenishment = true
                });
            });

        });

        return services;
    }
}