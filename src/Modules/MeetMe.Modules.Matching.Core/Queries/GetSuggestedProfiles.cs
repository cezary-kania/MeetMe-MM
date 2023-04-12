using MeetMe.Modules.Matching.Core.DTOs;
using MeetMe.Shared.Abstractions.Queries;

namespace MeetMe.Modules.Matching.Core.Queries;

public sealed record GetSuggestedProfiles(Guid UserId, uint Size) : IQuery<IEnumerable<SuggestedProfileDto>>;