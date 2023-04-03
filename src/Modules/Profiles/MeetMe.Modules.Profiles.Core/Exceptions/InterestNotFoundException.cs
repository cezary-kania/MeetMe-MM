using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Profiles.Core.Exceptions;

public class InterestNotFoundException : MeetMeException
{
    public InterestNotFoundException(Guid interestId) : base($"Interest with Id: \"{interestId}\" not found.")
    {
    }
}