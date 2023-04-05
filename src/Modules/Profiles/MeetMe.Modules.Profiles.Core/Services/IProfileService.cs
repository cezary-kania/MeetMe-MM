using MeetMe.Modules.Profiles.Core.DTOs;

namespace MeetMe.Modules.Profiles.Core.Services;

public interface IProfileService
{
    public Task CreateProfileAsync(Guid ownerId);
    public Task UpdateAsync(Guid ownerId, ProfileUpdateDto updateDto);
    public Task<ProfileDto> GetAsync(Guid ownerId);
    public Task<Boolean> ExistsAsync(Guid profileId);
    public Task<IEnumerable<ProfileImageDto>> GetImagesAsync(Guid ownerId);
    public Task AddImageAsync(Guid profileId, IEnumerable<byte[]> newImages);
    public Task RemoveImageAsync(Guid profileId, Guid imageId);
    public Task UpdateInterestsAsync(Guid profileId, IEnumerable<InterestDto> interestDtos);
}