using MeetMe.Shared.Abstractions.Events;

namespace MeetMe.Modules.Users.Shared.Events;

public record UserCreated(Guid UserId, string Email) : IEvent;