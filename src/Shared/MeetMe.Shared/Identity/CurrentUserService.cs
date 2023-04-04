using System.Security.Claims;
using MeetMe.Shared.Abstractions.Identity;
using Microsoft.AspNetCore.Http;

namespace MeetMe.Shared.Identity;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.Identity?.Name;
}