using MeetMe.Modules.Matching.Core.Commands;
using MeetMe.Modules.Profiles.Shared.Events;
using MeetMe.Shared.Abstractions.Dispatchers;
using MeetMe.Shared.Abstractions.Events;

namespace MeetMe.Modules.Matching.Api.EventHandlers;

public class ProfileUpdatedEventHandler : IEventHandler<ProfileUpdated>
{
    private readonly IDispatcher _dispatcher;

    public ProfileUpdatedEventHandler(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task HandleAsync(ProfileUpdated @event, CancellationToken cancellationToken = default)
    {
        await _dispatcher.SendAsync(
            new UpdateProfile(@event.UserId, @event.Active, @event.Name, @event.Age, @event.Gender),
            cancellationToken
        );
    }
}