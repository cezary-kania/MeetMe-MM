using MeetMe.Modules.Profiles.Core;
using MeetMe.Modules.Profiles.Core.DTOs;
using MeetMe.Modules.Profiles.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetMe.Modules.Profiles.Api;

public static class Extensions
{
    public static IServiceCollection AddProfilesModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore();
        return services;
    }

    public static IApplicationBuilder UseProfilesModule(this IApplicationBuilder app)
    {
        return app;
    }
    
    public static WebApplication ExposeProfilesApi(this WebApplication app)
    {
        const string availableInterestsRoute = "AvailableInterests";

        app.MapGet("api/profiles/available-interests", 
            async (IInterestsService interestsService) =>
                await interestsService.GetAvailableInterestsAsync()
        ).WithName(availableInterestsRoute);
        
        app.MapPost("api/profiles/available-interests", async (
            InterestDto interestDto, 
            IInterestsService interestsService) =>
        {
            await interestsService.AddInterestAsync(interestDto);
            return Results.CreatedAtRoute(availableInterestsRoute);
        });
        
        app.MapDelete("api/profiles/available-interests/{interestId:guid}", async (
            Guid interestId, 
            IInterestsService interestsService) =>
        {
            await interestsService.RemoveInterestAsync(interestId);
            return Results.NoContent();
        });
        
        return app;
    }
}