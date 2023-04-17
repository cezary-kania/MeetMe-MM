using MeetMe.Shared.Abstractions.Commands;

namespace MeetMe.Modules.Matching.Core.Commands;

public record UpdateProfile(Guid UserId, Boolean Active, string Name, uint Age, string Gender) : ICommand;
