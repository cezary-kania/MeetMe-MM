using System.Text.Json;
using MeetMe.Shared.Abstractions.Cache;
using Microsoft.Extensions.Caching.Distributed;

namespace MeetMe.Shared.Cache;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public RedisCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task CacheAsync(string key, object? data, TimeSpan timeToLive)
    {
        if (data is null)
        {
            return;
        }
        var serializedData = JsonSerializer.Serialize(data);
        await _distributedCache.SetStringAsync(key, serializedData, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = timeToLive
        });
    }

    public async Task<string?> GetAsync(string key)
    {
        var cachedData = await _distributedCache.GetStringAsync(key);
        return string.IsNullOrEmpty(cachedData) ? null : cachedData;
    }
}