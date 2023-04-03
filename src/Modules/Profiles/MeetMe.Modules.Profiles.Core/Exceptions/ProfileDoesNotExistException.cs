using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Profiles.Core.Exceptions;

public class ProfileDoesNotExistException : MeetMeException
{
    public ProfileDoesNotExistException(Guid ownerId) : base($"Profile with Id {ownerId} does not exist")
    {
    }
}