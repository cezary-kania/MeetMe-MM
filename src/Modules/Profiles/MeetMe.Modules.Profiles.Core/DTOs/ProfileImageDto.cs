namespace MeetMe.Modules.Profiles.Core.DTOs;

public class ProfileImageDto
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public uint DisplayOrder { get; set; }
    public byte[] BinaryData { get; set; }
}