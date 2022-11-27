namespace MeetMe.Modules.Users.Infrastructure.Auth;

public class AuthOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
    public TimeSpan? Expiry { get; set; }
}