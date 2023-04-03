using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Users.Application.Exceptions;

internal sealed class InvalidCredentialsExceptions : MeetMeException
{
    public InvalidCredentialsExceptions() : base($"Invalid credentials.")
    {
    }
}