namespace MeetMe.Modules.Profiles.Core.DTOs;

public class ProfileDto
{
    public Guid OwnerId { get; set; }
    public String Name { get; set; }
    public uint Age { get; set; }
    public string Gender { get; set; }
    public IEnumerable<InterestDto> Interests { get; set; }
}