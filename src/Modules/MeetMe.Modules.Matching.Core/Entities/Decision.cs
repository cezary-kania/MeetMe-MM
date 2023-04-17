using MeetMe.Modules.Matching.Core.Enums;

namespace MeetMe.Modules.Matching.Core.Entities;

internal class Decision
{
    public Guid UserId { get; private set; }
    public Guid ProfileId { get; private set; }
    public DecisionType DecisionType { get; private set; }
    public DateTime Time { get; private set; }
    
    public Decision(Guid userId, Guid profileId, DecisionType decisionType, DateTime time)
    {
        UserId = userId;
        ProfileId = profileId;
        DecisionType = decisionType;
        Time = time;
    }

    private Decision()
    {
    }
}
