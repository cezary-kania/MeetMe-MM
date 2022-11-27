using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Users.Domain.Exceptions;

public sealed class InvalidEmailException : MeetMeException
{
    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
    }
}