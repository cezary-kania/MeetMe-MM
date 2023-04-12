using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Matching.Core.Exceptions;

public class UserFilterNotDefinedException : MeetMeException
{
    public UserFilterNotDefinedException(Guid userId) : base($"Filters for {userId} not defined.")
    {
    }
}