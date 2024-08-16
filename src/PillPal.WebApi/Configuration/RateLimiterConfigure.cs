using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace PillPal.WebApi.Configuration;
public static class RateLimiterConfigure
{
    public static IServiceCollection AddRateLimiterServices(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = 429;
            
            options.AddFixedWindowLimiter("f", options =>
            {
                options.PermitLimit = 10;
                options.QueueLimit = 10;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.Window = TimeSpan.FromMinutes(1);
            });
            options.AddSlidingWindowLimiter("s", options =>
            {
                options.PermitLimit = 10;
                options.QueueLimit = 10;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.Window = TimeSpan.FromMinutes(1);
                options.SegmentsPerWindow = 10;
                options.AutoReplenishment = true;
            });
            options.AddConcurrencyLimiter("c", options =>
            {
                options.PermitLimit = 10;
                options.QueueLimit = 10;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
            options.AddTokenBucketLimiter("t", options =>
            {
                options.QueueLimit = 10;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.AutoReplenishment = true;
                options.ReplenishmentPeriod = TimeSpan.FromMinutes(1);
                options.TokenLimit = 10;
                options.TokensPerPeriod = 10;
            });

        });

        return services;
    }
}