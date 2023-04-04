using System.Reflection;
using MeetMe.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace MeetMe.Shared.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }

}