using MeetMe.Modules.Matching.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetMe.Modules.Matching.Core.DAL;

internal class MatchingDbContext : DbContext
{
    public DbSet<Match> Matches { get; set; }
    public DbSet<UserFilter> Filters { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    
    public MatchingDbContext(DbContextOptions<MatchingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("matching");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}