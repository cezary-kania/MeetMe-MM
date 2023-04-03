using MeetMe.Modules.Users.Domain.Entities;
using MeetMe.Modules.Users.Domain.valueTypes;

namespace MeetMe.Modules.Users.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(Email email);
    Task AddAsync(User user);
}