namespace MeetMe.Shared.Abstractions.Identity;

public interface ICurrentUserService
{
    public string? UserId { get; }
}