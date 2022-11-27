using MeetMe.Modules.Users.Domain.valueTypes;

namespace MeetMe.Modules.Users.Domain.Entities;

public sealed class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }

    private User()
    {
    }

    public User(UserId id, Email email, PasswordHash passwordHash)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
    }
}