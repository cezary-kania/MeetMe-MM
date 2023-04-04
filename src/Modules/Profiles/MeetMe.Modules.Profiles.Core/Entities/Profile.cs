using MeetMe.Modules.Profiles.Core.Constants;
using MeetMe.Modules.Profiles.Core.Exceptions;

namespace MeetMe.Modules.Profiles.Core.Entities;

internal sealed class Profile
{
    public Guid OwnerId { get; private set; } 
    public string? Name { get; private set; }
    public uint? Age { get; private set; }
    public Gender? Gender { get; private set; }
    public Boolean IsActive { get; private set; } = false;
    public List<Interest> Interests { get; private set; } = new List<Interest>();
    public List<ProfileImage> Images { get; private set; } = new List<ProfileImage>();

    private Profile()
    {
    }
    
    public Profile(Guid ownerId)
    {
        OwnerId = ownerId;
    }

    public void AddInterests(List<Interest> interests)
    {
        if (interests.Count + Interests.Count > 10)
        {
            throw new InvalidNumerOfInterestsException();
        }
        Interests.AddRange(interests);
    }
    
    public void AddImages(List<ProfileImage> images)
    {
        if (Images.Count + images.Count >= 5)
        {
            throw new ToManyImagesException();
        }

        uint displayOderCounter = (uint) Images.Count;
        foreach (ProfileImage profileImage in images)
        {
            profileImage.DisplayOrder = ++displayOderCounter;
        }
        Images.AddRange(images);
    }
    
    public void RemoveImage(Guid imageId)
    {
        var image = Images.Find(x => x.Id == imageId);
        if (image is null)
        {
            throw new ImageNotFoundException(imageId);
        }
        Images.Remove(image);
    }

    public void UpdateAge(uint age)
    {
        if (age is <= 13 or >= 100)
        {
            throw new InvalidAgeException();
        }
    }
    public void UpdateGender(Gender gender)
    {
        Gender = gender;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
        {
            throw new InvalidProfileNameException();
        }
        Name = name;
    }
    
    public void Activate()
    {
        IsActive = true;
    }
}