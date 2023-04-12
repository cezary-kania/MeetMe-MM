namespace MeetMe.Modules.Matching.Core.Entities;

internal sealed class Match
{
    public Guid Id { get; private set; }
    public Guid User1Id { get; private set; }
    public Guid User2Id { get; private set; }
    public bool IsActive { get; private set; } = true;

    private Match()
    {
    }

    public Match(Guid id, Guid user1Id, Guid user2Id, Boolean isActive)
    {
        Id = id;
        User1Id = user1Id;
        User2Id = user2Id;
        IsActive = isActive;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}