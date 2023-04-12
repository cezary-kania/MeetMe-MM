using System.Drawing;
using MeetMe.Modules.Matching.Core;
using MeetMe.Modules.Matching.Core.Queries;
using MeetMe.Shared.Abstractions.Dispatchers;
using MeetMe.Shared.Abstractions.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetMe.Modules.Matching.Api;

public static class Extensions
{
    public static IServiceCollection AddMatchingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore();
        return services;
    }

    public static IApplicationBuilder UseMatchingModule(this IApplicationBuilder app)
    {
        return app;
    }
    
    public static WebApplication ExposeMatchingApi(this WebApplication app)
    {
        app.MapGet("api/matching/suggested-profiles", async (
            [FromQuery] uint? size,
            [FromServices] ICurrentUserService currentUserService,
            [FromServices] IDispatcher dispatcher) =>
        {
            var userId = currentUserService.UserId;
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            var profiles = await dispatcher.QueryAsync(
                new GetSuggestedProfiles(Guid.Parse(userId), size ?? 10)
            );
            return Results.Ok(profiles);
        });
        
        app.MapPost("api/matching/{profileId:guid}/like", async (
            [FromServices] ICurrentUserService currentUserService,
            [FromServices] IDispatcher dispatcher) =>
        {
            var userId = currentUserService.UserId;
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            return Results.Ok();
        });
        
        return app;
    }
}