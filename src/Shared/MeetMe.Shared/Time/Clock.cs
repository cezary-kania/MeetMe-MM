using MeetMe.Shared.Abstractions.Services;

namespace MeetMe.Shared.Time;

public sealed class Clock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}