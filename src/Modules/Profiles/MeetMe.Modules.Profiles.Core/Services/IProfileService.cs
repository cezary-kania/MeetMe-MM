using MeetMe.Modules.Profiles.Core.DTOs;

namespace MeetMe.Modules.Profiles.Core.Services;

public interface IProfileService
{
    public Task CreateProfileAsync(Guid ownerId);

    public Task UpdateAsync(Guid ownerId, ProfileUpdateDto updateDto);
    public Task<ProfileDto> GetAsync(Guid ownerId);
}