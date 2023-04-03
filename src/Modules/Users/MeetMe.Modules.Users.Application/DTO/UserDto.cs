namespace MeetMe.Modules.Users.Application.DTO;

public class UserDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Email { get; set; } = String.Empty;
}