using MeetMe.Modules.Matching.Core.DAL;
using MeetMe.Modules.Matching.Core.Exceptions;
using MeetMe.Shared.Abstractions.Commands;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Matching.Core.Commands.Handlers;

internal class DeleteMatchHandler : ICommandHandler<DeleteMatch>
{
    private readonly MatchingDbContext _dbContext;

    public DeleteMatchHandler(MatchingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task HandleAsync(DeleteMatch command, CancellationToken cancellationToken = default)
    {
        var matchId = command.MatchId;
        var match = await _dbContext.Matches
            .FirstOrDefaultAsync(x => x.Id == matchId && x.IsActive, cancellationToken: cancellationToken);
        if (match is null)
        {
            throw new InvalidMatchIdException(matchId);
        }
        match.Deactivate();
        _dbContext.Update(match);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}