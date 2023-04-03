using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Profiles.Core.Exceptions;

public class InterestAlreadyExistException : MeetMeException
{
    public InterestAlreadyExistException(string name) : base($"Interest with name: {name} already exist.")
    {
    }
}