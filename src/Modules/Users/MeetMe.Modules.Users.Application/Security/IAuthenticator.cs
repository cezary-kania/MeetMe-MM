using MeetMe.Modules.Users.Application.DTO;

namespace MeetMe.Modules.Users.Application.Security;

public interface IAuthenticator
{
    JwtDTO CreateToken(Guid userId, string role);
}