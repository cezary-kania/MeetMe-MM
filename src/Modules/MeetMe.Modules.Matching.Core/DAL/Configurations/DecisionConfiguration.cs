using MeetMe.Modules.Matching.Core.Entities;
using MeetMe.Modules.Matching.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMe.Modules.Matching.Core.DAL.Configurations;

internal class DecisionConfiguration : IEntityTypeConfiguration<Decision>
{
    public void Configure(EntityTypeBuilder<Decision> builder)
    {
        builder.HasKey(x => new { x.ProfileId, x.UserId });
        builder.Property(x => x.DecisionType)
            .IsRequired()
            .HasMaxLength(20)
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<DecisionType>(x)
            );
        builder.Property(x => x.Time)
            .IsRequired();
    }
}