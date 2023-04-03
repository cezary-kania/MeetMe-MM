using MeetMe.Modules.Profiles.Core.DTOs;

namespace MeetMe.Modules.Profiles.Core.Services;

public interface IInterestsService
{
    public Task AddInterestAsync(InterestDto interestDto);
    public Task RemoveInterestAsync(Guid interestId);
    public Task<IEnumerable<InterestDto>> GetAvailableInterestsAsync();
}