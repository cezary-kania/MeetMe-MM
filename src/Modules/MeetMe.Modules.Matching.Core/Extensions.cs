using MeetMe.Modules.Matching.Core.DAL;
using MeetMe.Modules.Matching.Core.Services;
using MeetMe.Shared.Database;
using Microsoft.Extensions.DependencyInjection;

namespace MeetMe.Modules.Matching.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<MatchingDbContext>();
        services.AddScoped<IUserMatchService, UserMatchService>();
        services.AddScoped<IDecisionResultStore, HttpContextDecisionResultStore>();
        return services;
    }
}