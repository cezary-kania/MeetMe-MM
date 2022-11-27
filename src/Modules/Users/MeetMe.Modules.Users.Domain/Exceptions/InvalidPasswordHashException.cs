using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Users.Domain.Exceptions;

public class InvalidPasswordHashException : MeetMeException
{
    public InvalidPasswordHashException(string message) : base("Password is invalid.")
    {
    }
}