using MeetMe.Shared.Exceptions;

namespace MeetMe.Modules.Profiles.Core.Exceptions;

internal sealed class ImageNotFoundException : MeetMeException
{
    public ImageNotFoundException(Guid imageId) : base($"Image with Id: {imageId} was not found")
    {
    }
}