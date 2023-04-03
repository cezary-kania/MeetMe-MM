using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Profiles.Core.Exceptions;

internal sealed class InvalidAgeException : MeetMeException
{
    public InvalidAgeException() : base("Invalid age - must be between 13 and 100")
    {
    }
}