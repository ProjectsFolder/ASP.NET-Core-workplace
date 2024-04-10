using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Cache;

internal sealed class DistributedCache(IDistributedCache cache) : ICache
{
    private readonly IDistributedCache cache = cache;

    private readonly SemaphoreSlim locker = new(1, 1);

    public async Task<T?> GetOrSet<T>(
        string key,
        Func<Task<T>> getter,
        int expiredSeconds = 60,
        CancellationToken? cancellationToken = default)
        where T : class
    {
        T? result = null;
        var cancel = cancellationToken ?? CancellationToken.None;
        var cacheString = await cache.GetStringAsync(key, cancel);
        if (cacheString != null)
        {
            return JsonSerializer.Deserialize<T>(cacheString);
        }

        await locker.WaitAsync(cancel);
        try
        {
            cacheString = await cache.GetStringAsync(key, cancel);
            if (cacheString != null)
            {
                result = JsonSerializer.Deserialize<T>(cacheString);
            }
            
            if (result == null)
            {
                result = await getter();
                cacheString = JsonSerializer.Serialize(result);
                await cache.SetStringAsync(key, cacheString, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expiredSeconds),
                });
            }
        }
        finally
        {
            locker.Release();
        }

        return result;
    }
}
