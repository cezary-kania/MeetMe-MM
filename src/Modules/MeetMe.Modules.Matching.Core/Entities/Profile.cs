using MeetMe.Modules.Matching.Core.Enums;

namespace MeetMe.Modules.Matching.Core.Entities;

internal class Profile
{
    public Guid UserId { get; private set; }
    public Boolean Active { get; private set; }
    public string Name { get; private set; }
    public uint Age { get; private set; }
    public Gender Gender { get; private set; }

    private Profile()
    {
    }
    
    public Profile(Guid userId, bool active, string name, uint age, Gender gender)
    {
        UserId = userId;
        Active = active;
        Name = name;
        Age = age;
        Gender = gender;
    }
}