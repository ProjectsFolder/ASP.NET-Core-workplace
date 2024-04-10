using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using System.Text.Json;

namespace Infrastructure.Cache;

internal sealed class DistributedCache(IDistributedCache cache) : ICache
{
    private readonly IDistributedCache cache = cache;

    private readonly ConcurrentDictionary<object, SemaphoreSlim> locks = [];

    public async Task<T?> GetOrSet<T>(
        string key,
        Func<Task<T>> getter,
        int expiredSeconds = 60,
        CancellationToken? cancellationToken = default)
        where T : class
    {
        T? result = null;
        var cacheString = await cache.GetStringAsync(key, cancellationToken ?? CancellationToken.None);
        if (cacheString != null)
        {
            return JsonSerializer.Deserialize<T>(cacheString);
        }

        var locker = locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
        await locker.WaitAsync();
        try
        {
            cacheString = await cache.GetStringAsync(key, cancellationToken ?? CancellationToken.None);
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
