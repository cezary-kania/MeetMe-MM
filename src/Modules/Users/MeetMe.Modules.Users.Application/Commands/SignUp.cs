using MeetMe.Shared.Abstractions.Commands;

namespace MeetMe.Modules.Users.Application.Commands;

public record SignUp(Guid UserId, string Email, string Password) : ICommand;