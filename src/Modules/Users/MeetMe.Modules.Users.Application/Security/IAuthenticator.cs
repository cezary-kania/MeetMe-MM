using MeetMe.Modules.Users.Application.DTO;

namespace MeetMe.Modules.Users.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId);
}