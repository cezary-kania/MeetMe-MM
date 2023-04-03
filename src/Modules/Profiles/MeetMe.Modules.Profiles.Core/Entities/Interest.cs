namespace MeetMe.Modules.Profiles.Core.Entities;

public class Interest
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private Interest()
    {
    }
    
    public Interest(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}