using MeetMe.Modules.Profiles.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Profiles.Core.DAL;

internal sealed class ProfilesDbContext : DbContext
{
    public DbSet<Interest> Interests { get; set; }
    public DbSet<ProfileImage> ProfileImages { get; set; }
    public DbSet<Profile> Profiles { get; set; }

    public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("profiles");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}