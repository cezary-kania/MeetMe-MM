using MeetMe.Modules.Profiles.Core.DAL;
using MeetMe.Modules.Profiles.Core.Services;
using MeetMe.Shared.Database;
using Microsoft.Extensions.DependencyInjection;

namespace MeetMe.Modules.Profiles.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<ProfilesDbContext>();
        services.AddScoped<IInterestsService, InterestsService>();
        services.AddScoped<IProfileService, ProfileService>();
        return services;
    }
}