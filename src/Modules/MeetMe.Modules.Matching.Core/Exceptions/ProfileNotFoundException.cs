using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Matching.Core.Exceptions;

public class ProfileNotFoundException : MeetMeException
{
    public ProfileNotFoundException(Guid userId) : base($"Profile with userId {userId} not found.")
    {
    }
}