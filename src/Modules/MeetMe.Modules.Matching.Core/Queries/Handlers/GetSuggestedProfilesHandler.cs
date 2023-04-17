using MeetMe.Modules.Matching.Core.DAL;
using MeetMe.Modules.Matching.Core.DTOs;
using MeetMe.Modules.Matching.Core.Exceptions;
using MeetMe.Modules.Matching.Core.Services;
using MeetMe.Shared.Abstractions.Queries;
using MeetMe.Shared.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Matching.Core.Queries.Handlers;

internal sealed class GetSuggestedProfilesHandler : IQueryHandler<GetSuggestedProfiles, IEnumerable<SuggestedProfileDto>>
{
    private const uint DecisionExpirationTimeInDays = 5;
    private readonly IClock _clock;
    private readonly MatchingDbContext _dbContext;
    private readonly IUserMatchService _userMatchService;

    public GetSuggestedProfilesHandler(
        IClock clock, 
        MatchingDbContext dbContext, 
        IUserMatchService userMatchService)
    {
        _clock = clock;
        _dbContext = dbContext;
        _userMatchService = userMatchService;
    }

    public async Task<IEnumerable<SuggestedProfileDto>> HandleAsync(
        GetSuggestedProfiles query, 
        CancellationToken cancellationToken = default)
    {
        var userId = query.UserId;
        var userFilter = await _dbContext.Filters
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken: cancellationToken);
        if (userFilter is null)
        {
            throw new UserFilterNotDefinedException(userId);
        }
        var profileIdsToSkip = await GetProfileIdsToSkip(userId);
        var filteredProfiles = await _dbContext.Profiles
            .Where(x => x.UserId != userId && !profileIdsToSkip.Contains(x.UserId))
            .Where(x => x.Gender == userFilter.Gender)
            .Where(x => x.Active)
            .Where(x => x.Age <= userFilter.MaxAge && x.Age >= userFilter.MinAge)
            .Take((int) query.Size)
            .Select(x => new SuggestedProfileDto(x.UserId))
            .ToListAsync();
        return filteredProfiles;
    }

    private async Task<IEnumerable<Guid>> GetProfileIdsToSkip(Guid userId)
    {
        var profilesToSkip = new List<Guid>();
        var profilesFromDecisions = await GetProfileIdsFromActiveDecisions(userId);
        profilesToSkip.AddRange(profilesFromDecisions);
        var profilesFromMatches = await GetProfileIdsFromMatches(userId);
        profilesToSkip.AddRange(profilesFromMatches);
        return profilesToSkip;
    }

    private async Task<IEnumerable<Guid>> GetProfileIdsFromActiveDecisions(Guid userId)
        => await _dbContext.Decisions
            .Where(x => x.UserId == userId)
            .Where(x => x.Time > _clock.Now.Add(TimeSpan.FromDays(DecisionExpirationTimeInDays)))
            .Select(x => x.ProfileId)
            .ToListAsync();
    
    private async Task<IEnumerable<Guid>> GetProfileIdsFromMatches(Guid userId)
    {
        var matches = await _userMatchService.GetMatchesAsync(userId);
        return matches.Select(x => x.User1Id == userId ? x.User2Id : x.User1Id);
    }
}
