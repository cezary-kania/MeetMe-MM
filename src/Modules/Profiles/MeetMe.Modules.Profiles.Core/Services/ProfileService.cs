using MeetMe.Modules.Profiles.Core.Constants;
using MeetMe.Modules.Profiles.Core.DAL;
using MeetMe.Modules.Profiles.Core.DTOs;
using MeetMe.Modules.Profiles.Core.Entities;
using MeetMe.Modules.Profiles.Core.Exceptions;
using MeetMe.Modules.Profiles.Shared.Events;
using MeetMe.Shared.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Profiles.Core.Services;

internal sealed class ProfileService : IProfileService
{
    private readonly ProfilesDbContext _dbContext;
    private readonly IMessageBroker _messageBroker;

    public ProfileService(ProfilesDbContext dbContext, IMessageBroker messageBroker)
    {
        _dbContext = dbContext;
        _messageBroker = messageBroker;
    }

    public async Task CreateProfileAsync(Guid ownerId)
    {
        await _dbContext.Profiles.AddAsync(new Profile(ownerId));
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid ownerId, ProfileUpdateDto updateDto)
    {
        var profile = await GetProfileOrThrowNotExist(ownerId);
        profile.UpdateName(updateDto.Name);
        profile.UpdateAge(updateDto.Age);
        var gender = (Gender) Enum.Parse(typeof(Gender), updateDto.Gender);
        profile.UpdateGender(gender);
        if (!profile.IsActive)
        {
            profile.Activate();
        }
        _dbContext.Update(profile);
        await _dbContext.SaveChangesAsync();
        await _messageBroker.PublishAsync(
            new ProfileUpdated
                (
                    ownerId,
                    profile.IsActive,
                    profile.Name ?? string.Empty,
                    profile.Age ?? default(uint),
                    profile.Gender.ToString() ?? string.Empty
                )
            );
    }

    public async Task<ProfileDto> GetAsync(Guid ownerId)
    {
        var profile = await GetProfileOrThrowNotExist(ownerId);
        return profile.AsDto();
    }

    public async Task<bool> ExistsAsync(Guid profileId)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(x => x.OwnerId == profileId);
        return profile is not null;
    }

    public async Task<IEnumerable<ProfileImageDto>> GetImagesAsync(Guid ownerId)
    {
        var profile = await GetProfileOrThrowNotExist(ownerId);
        var images = await _dbContext.ProfileImages
            .Where(x => x.ProfileId == profile.OwnerId)
            .ToListAsync();
        return images.Select(x => x.AsDto());
    }
    
    public async Task AddImageAsync(Guid profileId, IEnumerable<byte[]> newImages)
    {
        var profile = await GetProfileOrThrowNotExist(profileId);
        var imagesToAdd = newImages
            .Select(x => new ProfileImage(Guid.NewGuid(), profileId, x))
            .ToList();
        profile.AddImages(imagesToAdd);
        _dbContext.Update(profile);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task RemoveImageAsync(Guid profileId, Guid imageId)
    {
        var profile = await GetProfileOrThrowNotExist(profileId);
        profile.RemoveImage(imageId);
        _dbContext.Update(profile);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateInterestsAsync(Guid profileId, IEnumerable<InterestDto> interestDtos)
    {
        var profile = await GetProfileOrThrowNotExist(profileId);
        var interests = interestDtos
            .Select(x => new Interest(x.Id, x.Name))
            .ToList();
        profile.AddInterests(interests);
        _dbContext.Update(profile);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<Profile> GetProfileOrThrowNotExist(Guid profileId)
    {
        var profile = await _dbContext.Profiles
            .Include(x => x.Interests)
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.OwnerId == profileId);
        return profile ?? throw new ProfileDoesNotExistException(profileId);
    }
}