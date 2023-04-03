using MeetMe.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace MeetMe.Shared.Events;

internal static class Extensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
            
        return services;
    }
}