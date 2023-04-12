namespace MeetMe.Modules.Matching.Core.Services;

public interface IDecisionResultStore
{
    void Set(Boolean isMatch);
    Boolean Get();
}