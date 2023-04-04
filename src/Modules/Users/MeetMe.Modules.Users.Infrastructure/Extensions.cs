using MeetMe.Modules.Users.Domain.Repositories;
using MeetMe.Modules.Users.Infrastructure.Auth;
using MeetMe.Modules.Users.Infrastructure.DAL;
using MeetMe.Modules.Users.Infrastructure.DAL.Repositories;
using MeetMe.Shared.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetMe.Modules.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<UsersDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddAuth(configuration);
        return services;
    }
}