namespace MeetMe.Shared.Abstractions.Cache;

public interface ICacheService
{
    Task CacheAsync(string key, object data, TimeSpan timeToLive);
    Task<string?> GetAsync(string key);
}