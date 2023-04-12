using MeetMe.Modules.Matching.Core.Enums;

namespace MeetMe.Modules.Matching.Core.Entities;

public record Decision(Guid ProfileId, string DecisionType);