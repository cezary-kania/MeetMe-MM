using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Profiles.Core.Exceptions;

internal sealed class ToManyImagesException : MeetMeException
{
    public ToManyImagesException() : base("User can add up to 5 images")
    {
    }
}