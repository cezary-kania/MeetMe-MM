using MeetMe.Modules.Matching.Core.DTOs;
using MeetMe.Shared.Abstractions.Queries;

namespace MeetMe.Modules.Matching.Core.Queries;

public record GetMatches(Guid UserId) : IQuery<IEnumerable<MatchDto>>;