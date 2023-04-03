namespace MeetMe.Modules.Profiles.Core.Entities;

internal sealed class ProfileImage
{
    public Guid Id { get; private set; }
    public Guid ProfileId { get; private set; }
    public uint DisplayOrder { get; private set; }
    public byte[] BinaryData { get; private set; }

    private ProfileImage()
    {
    }

    public ProfileImage(Guid id, Guid profileId, uint displayOrder, byte[] binaryData)
    {
        Id = id;
        ProfileId = profileId;
        DisplayOrder = displayOrder;
        BinaryData = binaryData;
    }
}