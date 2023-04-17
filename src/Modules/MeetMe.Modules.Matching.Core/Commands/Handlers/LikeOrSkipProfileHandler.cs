using System.Text.Json;
using MeetMe.Modules.Matching.Core.DAL;
using MeetMe.Modules.Matching.Core.Entities;
using MeetMe.Modules.Matching.Core.Enums;
using MeetMe.Modules.Matching.Core.Exceptions;
using MeetMe.Modules.Matching.Core.Services;
using MeetMe.Shared.Abstractions.Commands;
using MeetMe.Shared.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Matching.Core.Commands.Handlers;

internal sealed class LikeOrSkipProfileHandler : ICommandHandler<LikeOrSkipProfile>
{
    private const uint DecisionExpirationTimeInDays = 5;
    private readonly IClock _clock;
    private readonly MatchingDbContext _dbContext;
    private readonly IDecisionResultStore _decisionResultStore;

    public LikeOrSkipProfileHandler(IClock clock, MatchingDbContext dbContext,
        IDecisionResultStore decisionResultStore)
    {
        _clock = clock;
        _dbContext = dbContext;
        _decisionResultStore = decisionResultStore;
    }

    public async Task HandleAsync(LikeOrSkipProfile command, CancellationToken cancellationToken = default)
    {
        await UpdateDecisionsAsync(command);
        var (userId, profileId, decisionType) = command;
        if (Enum.Parse<DecisionType>(decisionType) == DecisionType.Like && await IsMatch(userId, profileId))
        {
            var match = new Match(Guid.NewGuid(), userId, profileId, true);
            await _dbContext.Matches.AddAsync(match, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _decisionResultStore.Set(true);
        }
        _decisionResultStore.Set(false);
    }

    private async Task UpdateDecisionsAsync(LikeOrSkipProfile command)
    {
        var (userId, profileId, decisionType) = command;
        var decisions = await GetActiveUserDecisionsForProfileAsync(userId, profileId);
        if (decisions.Any())
        {
            throw new CannotPerformLikeOrSkipOperation(userId, profileId);
        }

        var newDecision = new Decision(userId, profileId, Enum.Parse<DecisionType>(decisionType), _clock.Now);
        await _dbContext.Decisions.AddAsync(newDecision);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<Boolean> IsMatch(Guid userId, Guid profileId)
    {
        var decisions = await GetActiveUserDecisionsForProfileAsync(userId, profileId);
        return decisions.Any() && decisions.Last().DecisionType == DecisionType.Like;
    }

    private async Task<IEnumerable<Decision>> GetActiveUserDecisionsForProfileAsync(Guid userId, Guid profileId)
        => await _dbContext.Decisions
            .Where(x => x.UserId == userId)
            .Where(x => x.ProfileId == profileId)
            .Where(x => x.Time > _clock.Now.Add(TimeSpan.FromDays(DecisionExpirationTimeInDays)))
            .OrderByDescending(x => x.Time)
            .ToListAsync();
}