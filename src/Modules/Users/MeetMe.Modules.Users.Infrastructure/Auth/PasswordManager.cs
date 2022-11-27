using MeetMe.Modules.Users.Application.Security;
using MeetMe.Modules.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MeetMe.Modules.Users.Infrastructure.Auth;

public class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordManager(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Secure(string password)
        => _passwordHasher.HashPassword(default, password);

    public bool Validate(string password, string securedPassword)
        => _passwordHasher.VerifyHashedPassword(default, securedPassword, password)
           == PasswordVerificationResult.Success;
}