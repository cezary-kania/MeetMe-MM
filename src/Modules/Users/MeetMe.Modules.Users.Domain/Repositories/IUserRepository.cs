using MeetMe.Modules.Users.Domain.Entities;
using MeetMe.Modules.Users.Domain.valueTypes;

namespace MeetMe.Modules.Users.Domain.Repositories;

internal interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(UserId id);
    Task AddAsync(User user);
}