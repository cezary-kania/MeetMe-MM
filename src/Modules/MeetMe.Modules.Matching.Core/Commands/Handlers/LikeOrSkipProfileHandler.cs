using System.Text.Json;
using MeetMe.Modules.Matching.Core.DAL;
using MeetMe.Modules.Matching.Core.Entities;
using MeetMe.Modules.Matching.Core.Enums;
using MeetMe.Modules.Matching.Core.Exceptions;
using MeetMe.Modules.Matching.Core.Services;
using MeetMe.Shared.Abstractions.Cache;
using MeetMe.Shared.Abstractions.Commands;

namespace MeetMe.Modules.Matching.Core.Commands.Handlers;

internal sealed class LikeOrSkipProfileHandler : ICommandHandler<LikeOrSkipProfile>
{
    private readonly ICacheService _cacheService;
    private readonly MatchingDbContext _dbContext;
    private readonly IDecisionResultStore _decisionResultStore;

    public LikeOrSkipProfileHandler(ICacheService cacheService, MatchingDbContext dbContext,
        IDecisionResultStore decisionResultStore)
    {
        _cacheService = cacheService;
        _dbContext = dbContext;
        _decisionResultStore = decisionResultStore;
    }

    public async Task HandleAsync(LikeOrSkipProfile command, CancellationToken cancellationToken = default)
    {
        await UpdateDecisionsAsync(command);
        var (userId, profileId, decisionType) = command;
        if (decisionType == DecisionType.Like && await IsMatch(userId, profileId))
        {
            var match = new Match(Guid.NewGuid(), userId, profileId, true);
            await _dbContext.Matches.AddAsync(match);
            await _dbContext.SaveChangesAsync();
            _decisionResultStore.Set(true);
        }
        _decisionResultStore.Set(false);
    }

    private async Task UpdateDecisionsAsync(LikeOrSkipProfile command)
    {
        var (userId, profileId, decisionType) = command;
        var decisions = (await GetUserDecisionsAsync(userId)).ToList();
        if (decisions.Any(x => x.ProfileId == profileId))
        {
            throw new CannotPerformLikeOrSkipOperation(userId, profileId);
        }
        decisions.Add(new Decision(profileId, decisionType.ToString()));
        var decisionsCacheKey = GetCacheKey(userId);
        await _cacheService.CacheAsync(decisionsCacheKey, decisions, TimeSpan.FromDays(5));
    }

    private async Task<Boolean> IsMatch(Guid userId, Guid profileId)
    {
        return (await GetUserDecisionsAsync(profileId))
            .Any(x => x.ProfileId == userId && x.DecisionType == DecisionType.Like.ToString());
    }

    private string GetCacheKey(Guid userId) => $"matches-decisions-{userId}";

    private async Task<IEnumerable<Decision>> GetUserDecisionsAsync(Guid userId)
    {
        var key = GetCacheKey(userId);
        var serializedDecisions = await _cacheService.GetAsync(key);
        if (serializedDecisions is null)
        {
            return Enumerable.Empty<Decision>();
        }
        return JsonSerializer.Deserialize<IEnumerable<Decision>>(serializedDecisions) ?? Enumerable.Empty<Decision>();
    }

}