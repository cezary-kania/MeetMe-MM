using MeetMe.Shared.Abstractions.Events;

namespace MeetMe.Modules.Profiles.Shared.Events;

public record ProfileUpdated(Guid UserId, Boolean Active, string Name, uint Age, string Gender) : IEvent;