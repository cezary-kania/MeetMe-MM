using MeetMe.Shared.Abstractions.Dispatchers;
using MeetMe.Shared.Abstractions.Identity;
using MeetMe.Shared.Abstractions.Services;
using MeetMe.Shared.Auth;
using MeetMe.Shared.Cache;
using MeetMe.Shared.Commands;
using MeetMe.Shared.Database;
using MeetMe.Shared.Dispatchers;
using MeetMe.Shared.Events;
using MeetMe.Shared.Identity;
using MeetMe.Shared.Messaging;
using MeetMe.Shared.Queries;
using MeetMe.Shared.Swagger;
using MeetMe.Shared.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetMe.Shared;

public static class Extensions
{
    public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCommands();
        services.AddEvents();
        services.AddQueries();
        services.AddMessaging();
        services.AddPostgres(configuration);
        services.AddRedisCache(configuration);
        services.AddAuth(configuration);
        services.AddSingleton<IClock, Clock>();
        services.AddSingleton<IDispatcher, InMemoryDispatcher>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSwagger();
        return services;
    }

    public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);
        return options;
    }
}