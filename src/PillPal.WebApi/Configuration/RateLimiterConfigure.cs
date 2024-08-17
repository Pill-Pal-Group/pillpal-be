using Microsoft.AspNetCore.RateLimiting;

namespace PillPal.WebApi.Configuration;
public static class RateLimiterConfigure
{
    public const string BucketLimiter = "bucket";
    public static IServiceCollection AddRateLimiterServices(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            
            options.AddTokenBucketLimiter(BucketLimiter, options =>
            {
                options.ReplenishmentPeriod = TimeSpan.FromMinutes(1);
                options.TokenLimit = 100;
                options.TokensPerPeriod = 10;
            });
        });

        return services;
    }
}