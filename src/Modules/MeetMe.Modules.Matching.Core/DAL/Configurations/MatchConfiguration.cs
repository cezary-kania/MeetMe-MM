using MeetMe.Modules.Matching.Core.Entities;
using MeetMe.Modules.Matching.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMe.Modules.Matching.Core.DAL.Configurations;

internal class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.User1Id)
            .IsRequired();
        builder.Property(x => x.User2Id)
            .IsRequired();
        builder.Property(x => x.IsActive)
            .IsRequired();
    }
}