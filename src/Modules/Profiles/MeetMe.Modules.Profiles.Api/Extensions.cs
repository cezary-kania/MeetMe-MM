using MeetMe.Modules.Profiles.Core;
using MeetMe.Modules.Profiles.Core.DTOs;
using MeetMe.Modules.Profiles.Core.Services;
using MeetMe.Shared.Abstractions.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        )
            .WithName(availableInterestsRoute)
            .RequireAuthorization();
        
        app.MapPost("api/profiles/available-interests", async (
            InterestDto interestDto, 
            [FromServices] IInterestsService interestsService) =>
        {
            await interestsService.AddInterestAsync(interestDto);
            return Results.CreatedAtRoute(availableInterestsRoute);
        }).RequireAuthorization();
        
        app.MapDelete("api/profiles/available-interests/{interestId:guid}", async (
            Guid interestId, 
            [FromServices] IInterestsService interestsService) =>
        {
            await interestsService.RemoveInterestAsync(interestId);
            return Results.NoContent();
        }).RequireAuthorization();
        
        app.MapGet("api/profiles/{profileId:guid}", 
            async (Guid profileId, IProfileService profileService) =>
                await profileService.GetAsync(profileId)
        ).RequireAuthorization();
        
        app.MapPatch("api/profiles",
        async (
            [FromBody] ProfileUpdateDto updateDto,
            [FromServices] ICurrentUserService currentUserService,
            [FromServices] IProfileService profileService
            ) =>
        {
            if (Guid.TryParse(currentUserService.UserId, out var profileId))
            {
                await profileService.UpdateAsync(profileId, updateDto);
                return Results.NoContent();
            }
            return Results.BadRequest();
        }).RequireAuthorization();
        
        app.MapGet("api/profiles/{profileId:guid}/images",
        async (Guid profileId, IProfileService profileService) 
            => await profileService.GetImagesAsync(profileId)
            ).RequireAuthorization();
        
        app.MapPost("api/profiles/images",
        async (
            [FromForm] List<IFormFile> images,
            [FromServices] ICurrentUserService currentUserService,
            [FromServices] IProfileService profileService) =>
        {
            if (!Guid.TryParse(currentUserService.UserId, out var profileId) || images.Count is > 5 or < 1)
            {
                return Results.BadRequest();
            }
            var processedImages = await Task.WhenAll(
                images.Select(async x =>
            {
                using var memoryStream = new MemoryStream();
                await x.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }));
            await profileService.AddImageAsync(profileId, processedImages);
            return Results.NoContent();
        }).RequireAuthorization();

        app.MapDelete("api/profiles/images/{imageId:guid}",
        async (
            [FromRoute] Guid imageId,
            [FromServices] ICurrentUserService currentUserService,
            [FromServices] IProfileService profileService) =>
        {
            if(!Guid.TryParse(currentUserService.UserId, out var profileId))
            {
                return Results.BadRequest();
            }
            await profileService.RemoveImageAsync(profileId, imageId);
            return Results.NoContent();
        }).RequireAuthorization();
        
        app.MapPut("api/profiles/interests",
        async (
            [FromBody] IEnumerable<InterestDto> interestDtos,
            [FromServices] ICurrentUserService currentUserService,
            [FromServices] IProfileService profileService) =>
        {
            if(!Guid.TryParse(currentUserService.UserId, out var profileId))
            {
                return Results.BadRequest();
            }
            await profileService.UpdateInterestsAsync(profileId, interestDtos);
            return Results.NoContent();
        }).RequireAuthorization();
        
        return app;
    }
}