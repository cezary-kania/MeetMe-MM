using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Profiles.Core.Exceptions;

public class InvalidProfileNameException : MeetMeException
{
    public InvalidProfileNameException() : base("Profile name is invalid")
    {
    }
}