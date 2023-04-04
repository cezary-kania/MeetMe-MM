using MeetMe.Modules.Profiles.Core.Entities;

namespace MeetMe.Modules.Profiles.Core.DTOs;

internal static class Extensions
{
    internal static ProfileDto AsDto(this Profile profile)
    {
        return new ProfileDto
        {
            OwnerId = profile.OwnerId,
            Name = profile.Name,
            Age = profile.Age,
            Gender = profile.Gender.ToString(),
            Interests = profile.Interests.Select( x => x.AsDto())
        };
    }
    internal static InterestDto AsDto(this Interest interest)
    {
        return new InterestDto
        {
            Id = interest.Id,
            Name = interest.Name,
        };
    }
    internal static ProfileImageDto AsDto(this ProfileImage profileImage)
    {
        return new ProfileImageDto
        {
            Id = profileImage.Id,
            ProfileId = profileImage.ProfileId,
            DisplayOrder = profileImage.DisplayOrder ?? default(uint),
            BinaryData = profileImage.BinaryData,
        };
    }
}