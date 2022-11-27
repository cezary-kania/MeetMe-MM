using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Users.Domain.Exceptions;

public class EmptyUserIdException : MeetMeException
{
    public EmptyUserIdException() : base("User Id cannot be empty.")
    {
    }
}