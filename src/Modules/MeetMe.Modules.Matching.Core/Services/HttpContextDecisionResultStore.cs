using Microsoft.AspNetCore.Http;

namespace MeetMe.Modules.Matching.Core.Services;

public class HttpContextDecisionResultStore : IDecisionResultStore
{
    private const string MatchKey = "match";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextDecisionResultStore(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Set(bool isMatch)
        => _httpContextAccessor.HttpContext?.Items.TryAdd(MatchKey, isMatch);

    public bool Get()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return false;
        }
        if (_httpContextAccessor.HttpContext.Items.TryGetValue(MatchKey, out var isMatch))
        {
            return isMatch is not null && Convert.ToBoolean(isMatch);
        }
        return false;
    }
}