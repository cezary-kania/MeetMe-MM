using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeetMe.Modules.Users.Application.Security;
using MeetMe.Modules.Users.Application.DTO;
using MeetMe.Shared.Abstractions.Services;
using MeetMe.Shared.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MeetMe.Modules.Users.Infrastructure.Auth;

internal sealed class Authenticator : IAuthenticator
{
    private readonly IClock _clock;
    private readonly string _issuer;
    private readonly TimeSpan _expiry;
    private readonly string _audience;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new JwtSecurityTokenHandler();

    public Authenticator(IOptions<AuthOptions> options, IClock clock)
    {
        _clock = clock;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(options.Value.SigningKey)),
            SecurityAlgorithms.HmacSha256);
    }
    
    public JwtDto CreateToken(Guid userId)
    {
        var now = _clock.Now;
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
        };

        var expires = now.Add(_expiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);

        return new JwtDto(token);
    }
}