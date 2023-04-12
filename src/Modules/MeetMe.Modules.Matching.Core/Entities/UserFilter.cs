using MeetMe.Modules.Matching.Core.Enums;

namespace MeetMe.Modules.Matching.Core.Entities;

internal sealed class UserFilter
{
    public Guid UserId { get; private set; }
    public uint MinAge { get; private set; }
    public uint MaxAge { get; private set; }
    public Gender Gender { get; private set; }

    private UserFilter()
    {
    }

    public UserFilter(Guid userId, uint minAge, uint maxAge, Gender gender)
    {
        UserId = userId;
        MinAge = minAge;
        MaxAge = maxAge;
        Gender = gender;
    }
}