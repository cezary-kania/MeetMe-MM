namespace MeetMe.Shared.Auth;

public sealed class AuthOptions
{
    public string Issuer { get; set; } = String.Empty;
    public string Audience { get; set; } = String.Empty;
    public string SigningKey { get; set; } = String.Empty;
    public TimeSpan? Expiry { get; set; }
}