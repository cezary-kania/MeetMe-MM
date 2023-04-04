using MeetMe.Modules.Users.Application.Commands;
using MeetMe.Modules.Users.Application.Queries;
using MeetMe.Modules.Users.Application.Security;
using MeetMe.Modules.Users.Infrastructure;
using MeetMe.Shared.Abstractions.Dispatchers;
using MeetMe.Shared.Abstractions.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeetMe.Modules.Users.Api;

public static class Extensions
{
    private const string MeRoute = "me";
    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        return services;
    }

    public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
    {
        return app;
    }
    
    public static WebApplication ExposeUsersApi(this WebApplication app)
    {
        app.MapGet("api/users/me", async (
            [FromServices] ICurrentUserService currentUserService, 
            [FromServices] IDispatcher dispatcher) =>
        {
            var userId = currentUserService.UserId;
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            var userDto = await dispatcher.QueryAsync(new GetUser {UserId = Guid.Parse(userId)});
            return Results.Ok(userDto);
        })
            .RequireAuthorization()
            .WithName(MeRoute);

        app.MapPost("api/users/sign-in", async (
            SignIn command, 
            [FromServices] IDispatcher dispatcher, 
            [FromServices] ITokenStorage tokenStorage) =>
        {
            await dispatcher.SendAsync(command);
            var jwt = tokenStorage.Get();
            return Results.Ok(jwt);
        });
        
        app.MapPost("api/users/sign-up", async (SignUp command, [FromServices] IDispatcher dispatcher) =>
        {
            command = command with {UserId = Guid.NewGuid()};
            await dispatcher.SendAsync(command);
            return Results.CreatedAtRoute(MeRoute);
        });
        
        return app;
    }
}