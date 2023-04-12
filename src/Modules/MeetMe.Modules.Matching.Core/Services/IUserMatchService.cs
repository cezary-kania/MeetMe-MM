using MeetMe.Modules.Matching.Core.Entities;

namespace MeetMe.Modules.Matching.Core.Services;

internal interface IUserMatchService
{
    public Task<IEnumerable<Match>> GetMatchesAsync(Guid userId); 
}