using System.Diagnostics.Tracing;
using MeetMe.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace MeetMe.Shared.Messaging;

internal static class Extensions
{
    private const string SectionName = "messaging";
        
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
        services.AddTransient<IAsyncEventDispatcher, AsyncEventDispatcher>();
        services.AddSingleton<IEventChannel, EventChannel>();
        services.AddHostedService<EventDispatcherJob>();
            
        return services;
    }
}