using MeetMe.Shared.Abstractions.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetMe.Shared.Cache;

public static class Extensions
{
    private const string SectionName = "RedisCache";
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["RedisCache:ConnectionString"];
        });
        services.AddScoped<ICacheService, RedisCacheService>();
        return services;
    }
}