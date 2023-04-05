using System.Collections;

namespace MeetMe.Modules.Profiles.Core.DTOs;

public class UpdateProfileInterestsDto
{
    public IEnumerable<InterestDto> Interests { get; set; } = Enumerable.Empty<InterestDto>();
}