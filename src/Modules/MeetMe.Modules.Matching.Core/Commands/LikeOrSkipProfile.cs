using MeetMe.Modules.Matching.Core.Enums;
using MeetMe.Shared.Abstractions.Commands;

namespace MeetMe.Modules.Matching.Core.Commands;

public record LikeOrSkipProfile(Guid UserId, Guid ProfileId, DecisionType DecisionType) : ICommand;