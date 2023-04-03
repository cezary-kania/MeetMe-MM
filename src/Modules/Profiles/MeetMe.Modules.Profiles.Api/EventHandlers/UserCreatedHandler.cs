using MeetMe.Modules.Profiles.Api.Exceptions;
using MeetMe.Modules.Profiles.Core.Services;
using MeetMe.Modules.Users.Shared.Events;
using MeetMe.Shared.Abstractions.Events;

namespace MeetMe.Modules.Profiles.Api.EventHandlers;

public class UserCreatedHandler : IEventHandler<UserCreated>
{
    private readonly IProfileService _profileService;

    public UserCreatedHandler(IProfileService profileService)
    {
        _profileService = profileService;
    }

    public async Task HandleAsync(UserCreated @event, CancellationToken cancellationToken = default)
    {
        var ownerId = @event.UserId;
        if (await _profileService.GetAsync(ownerId) is not null)
        {
            throw new ProfileAlreadyExistException(ownerId);
        }
        await _profileService.CreateProfileAsync(ownerId);
    }
}