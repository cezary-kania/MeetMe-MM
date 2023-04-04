namespace MeetMe.Modules.Profiles.Core.DTOs;

public class AddImagesDto
{
    public Guid ProfileId { get; set; }
    public IEnumerable<NewImageDto> Images { get; set; } = Enumerable.Empty<NewImageDto>();
}