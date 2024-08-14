namespace PillPal.Infrastructure.Cache;

public class CacheService(IDistributedCache cache, IOptions<CacheSettings> settings)
    : ICacheService
{
    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<T?> GetAsync<T>(string key)
    {
        var cacheData = await cache.GetStringAsync(key);

        if (cacheData == null)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(cacheData);
    }

    public async Task RemoveAsync(string key)
    {
        await cache.RemoveAsync(key);
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var options = new DistributedCacheEntryOptions();

        if (settings.Value.AbsoluteExpiration > 0)
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(settings.Value.AbsoluteExpiration);
        }

        if (settings.Value.SlidingExpiration > 0)
        {
            options.SlidingExpiration = TimeSpan.FromMinutes(settings.Value.SlidingExpiration);
        }

        await cache.SetStringAsync(key, JsonSerializer.Serialize(value, jsonSerializerOptions), options);
    }
}
