using MeetMe.Modules.Matching.Core.DTOs;
using MeetMe.Modules.Matching.Core.Services;
using MeetMe.Shared.Abstractions.Queries;

namespace MeetMe.Modules.Matching.Core.Queries.Handlers;

internal class GetMachesHandler : IQueryHandler<GetMatches, IEnumerable<MatchDto>>
{
    private readonly IUserMatchService _userMatchService;

    public GetMachesHandler(IUserMatchService userMatchService)
    {
        _userMatchService = userMatchService;
    }

    public async Task<IEnumerable<MatchDto>> HandleAsync(GetMatches query, CancellationToken cancellationToken = default)
    {
        var matches = await _userMatchService.GetMatchesAsync(query.UserId);
        return matches.Select(x => new MatchDto(x.Id, x.User1Id, x.User2Id));
    }
}