using MeetMe.Shared.Abstractions.Commands;

namespace MeetMe.Modules.Matching.Core.Commands;

public record DeleteMatch(Guid MatchId) : ICommand;
