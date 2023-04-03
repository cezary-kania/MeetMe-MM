using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Profiles.Api.Exceptions;

public class ProfileAlreadyExistException : MeetMeException
{
    public ProfileAlreadyExistException(Guid ownerId) : base($"Profile for ${ownerId} already exists.")
    {
    }
}