using MeetMe.Modules.Users.Application.DTO;
using MeetMe.Shared.Abstractions.Queries;

namespace MeetMe.Modules.Users.Application.Queries;

public class GetUser : IQuery<UserDto>
{
    public Guid UserId { get; set; }
}