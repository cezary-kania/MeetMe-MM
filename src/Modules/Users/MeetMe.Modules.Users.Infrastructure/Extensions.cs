using MeetMe.Modules.Users.Infrastructure.Auth;
using MeetMe.Modules.Users.Infrastructure.DAL;
using MeetMe.Shared.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetMe.Modules.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<UsersDbContext>();
        services.AddAuth(configuration);
        return services;
    }
}