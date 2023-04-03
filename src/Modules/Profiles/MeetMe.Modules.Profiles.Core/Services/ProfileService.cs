using MeetMe.Modules.Profiles.Core.Constants;
using MeetMe.Modules.Profiles.Core.DAL;
using MeetMe.Modules.Profiles.Core.DTOs;
using MeetMe.Modules.Profiles.Core.Entities;
using MeetMe.Modules.Profiles.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Profiles.Core.Services;

internal sealed class ProfileService : IProfileService
{
    private readonly ProfilesDbContext _dbContext;

    public ProfileService(ProfilesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateProfileAsync(Guid ownerId)
    {
        await _dbContext.Profiles.AddAsync(new Profile(ownerId));
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid ownerId, ProfileUpdateDto updateDto)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(x => x.OwnerId == ownerId);
        if (profile is null)
        {
            throw new ProfileDoesNotExistException(ownerId);
        }
        profile.UpdateAge(updateDto.Age);
        var gender = (Gender) Enum.Parse(typeof(Gender), updateDto.Gender);
        profile.UpdateGender(gender);
        _dbContext.Update(profile);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProfileDto?> GetAsync(Guid ownerId)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(x => x.OwnerId == ownerId);
        return profile is not null ? profile.AsDto() : null;
    }
}