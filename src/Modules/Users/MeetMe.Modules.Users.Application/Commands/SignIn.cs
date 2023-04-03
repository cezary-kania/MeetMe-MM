using MeetMe.Shared.Abstractions.Commands;

namespace MeetMe.Modules.Users.Application.Commands;

public record SignIn(string Email, string Password) : ICommand;