using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Matching.Core.Exceptions;

internal sealed class InvalidMatchIdException : MeetMeException
{
    public InvalidMatchIdException(Guid matchId) : base($"Could not find active match with Id {matchId}.")
    {
    }
}