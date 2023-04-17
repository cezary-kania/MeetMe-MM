using MeetMe.Modules.Matching.Core.Entities;
using MeetMe.Modules.Matching.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMe.Modules.Matching.Core.DAL.Configurations;

internal class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.Name)
            .HasMaxLength(100);
        builder.Property(x => x.Gender)
            .HasMaxLength(100)
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<Gender>(x)
            );
        builder.Property(x => x.Age)
            .HasMaxLength(5)
            .IsRequired();
        builder.Property(x => x.Active)
            .IsRequired();
    }
}