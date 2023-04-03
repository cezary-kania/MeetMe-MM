using MeetMe.Modules.Users.Application.DTO;
using MeetMe.Modules.Users.Application.Queries;
using MeetMe.Modules.Users.Domain.valueTypes;
using MeetMe.Modules.Users.Infrastructure.DAL;
using MeetMe.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Users.Infrastructure.Queries.Handlers;

public class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly UsersDbContext _dbContext;

    public GetUserHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
    {
        var userId = new UserId(query.UserId);
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == userId);
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email
        };
    }
}