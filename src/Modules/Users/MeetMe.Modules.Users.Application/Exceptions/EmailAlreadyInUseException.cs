using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Users.Application.Exceptions;

internal sealed class EmailAlreadyInUseException : MeetMeException
{
    public EmailAlreadyInUseException(string email) : base($"User with email: '{email}' already exists.")
    {
    }
}