using MeetMe.Modules.Users.Domain.Entities;
using MeetMe.Modules.Users.Domain.Repositories;
using MeetMe.Modules.Users.Domain.valueTypes;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Users.Infrastructure.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UsersDbContext _dbContext;
    private readonly DbSet<User> _users;

    public UserRepository(UsersDbContext dbContext)
    {
        _users = dbContext.Users;
        _dbContext = dbContext;
    }

    public Task<User> GetByIdAsync(UserId id)
        => _users.SingleOrDefaultAsync(x => x.Id == id);

    public Task<User> GetByEmailAsync(Email email)
        => _users.SingleOrDefaultAsync(x => x.Email == email);

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}