using MeetMe.Modules.Matching.Core.DAL;
using MeetMe.Modules.Matching.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Matching.Core.Services;

internal sealed class UserMatchService : IUserMatchService
{
    private readonly MatchingDbContext _dbContext;

    public UserMatchService(MatchingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Match>> GetMatchesAsync(Guid userId)
    {
        var matches = await _dbContext
            .Matches
            .Where(x => x.User1Id == userId || x.User2Id == userId)
            .Where(x => x.IsActive)
            .ToListAsync();
        return matches;
    }
}