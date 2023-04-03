using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Profiles.Core.Exceptions;

internal sealed class InvalidNumerOfInterestsException : MeetMeException
{
    public InvalidNumerOfInterestsException() : base("Interests size must be between 0 and 10")
    {
    }
}