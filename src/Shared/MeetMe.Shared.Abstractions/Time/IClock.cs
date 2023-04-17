namespace MeetMe.Shared.Abstractions.Services;

public interface IClock
{
    DateTime Now { get; }
}