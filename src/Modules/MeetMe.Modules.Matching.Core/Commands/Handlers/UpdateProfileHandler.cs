using MeetMe.Modules.Matching.Core.DAL;
using MeetMe.Modules.Matching.Core.Entities;
using MeetMe.Modules.Matching.Core.Enums;
using MeetMe.Modules.Matching.Core.Exceptions;
using MeetMe.Shared.Abstractions.Commands;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Matching.Core.Commands.Handlers;

internal sealed class UpdateProfileHandler : ICommandHandler<UpdateProfile>
{
    private readonly MatchingDbContext _dbContext;

    public UpdateProfileHandler(MatchingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task HandleAsync(UpdateProfile command, CancellationToken cancellationToken = default)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(x => x.UserId == command.UserId, cancellationToken: cancellationToken);
        var gender = Enum.Parse<Gender>(command.Gender);
        if (profile is null && !command.Active)
        {
            throw new ProfileNotFoundException(command.UserId);
        }
        if (profile is null && command.Active)
        {
            profile = new Profile(command.UserId, command.Active, command.Name, command.Age, gender);
            await _dbContext.Profiles.AddAsync(profile);
        } else if (profile is not null && command.Active)
        {
            _dbContext.Profiles.Update(profile);
        }
        else
        {
            _dbContext.Profiles.Remove(profile);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}