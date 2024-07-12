namespace PillPal.Application.Common.Interfaces.Caches;

public class CacheSettings
{
    /// <summary>
    /// Cache sliding expiration in minutes
    /// </summary>
    public int SlidingExpiration { get; set; }

    /// <summary>
    /// Cache absolute expiration in minutes
    /// </summary>
    public int AbsoluteExpiration { get; set; }
}

public interface ICacheService
{
    /// <summary>
    /// Get value from cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache key</param>
    Task<T?> GetAsync<T>(string key);

    /// <summary>
    /// Set value to cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="value">Cache value to set</param>
    Task SetAsync<T>(string key, T value);

    /// <summary>
    /// Remove value from cache
    /// </summary>
    /// <param name="key">Cache key</param>
    Task RemoveAsync(string key);
}
