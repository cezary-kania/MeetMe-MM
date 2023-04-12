using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Matching.Core.Exceptions;

public class CannotPerformLikeOrSkipOperation : MeetMeException
{
    public CannotPerformLikeOrSkipOperation(Guid userId, Guid profileId) 
        : base($"User {userId}, can not like or skip profile {profileId}")
    {
    }
}