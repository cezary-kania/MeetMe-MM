using MeetMe.Modules.Profiles.Core.DAL;
using MeetMe.Modules.Profiles.Core.DTOs;
using MeetMe.Modules.Profiles.Core.Entities;
using MeetMe.Modules.Profiles.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Profiles.Core.Services;

internal sealed class InterestsService : IInterestsService
{
    private readonly ProfilesDbContext _dbContext;

    public InterestsService(ProfilesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddInterestAsync(InterestDto interestDto)
    {
        var interestExist = await _dbContext.Interests
            .AnyAsync(x => x.Name == interestDto.Name);
        if (interestExist)
        {
            throw new InterestAlreadyExistException(interestDto.Name);
        }

        var interest = new Interest(Guid.NewGuid(), interestDto.Name);
        await _dbContext.Interests.AddAsync(interest);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveInterestAsync(Guid interestId)
    {
        var interest = await _dbContext.Interests
            .FirstOrDefaultAsync(x => x.Id == interestId);
        if (interest is null)
        {
            throw new InterestNotFoundException(interestId);
        }

        _dbContext.Interests.Remove(interest);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<InterestDto>> GetAvailableInterestsAsync()
    {
        var interests = await _dbContext.Interests.ToListAsync();
        return interests.Select(x => new InterestDto { Id = x.Id, Name = x.Name });
    }
}