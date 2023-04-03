using MeetMe.Modules.Profiles.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMe.Modules.Profiles.Core.DAL.Configurations;

internal sealed class ProfileImageConfiguration : IEntityTypeConfiguration<ProfileImage>
{
    public void Configure(EntityTypeBuilder<ProfileImage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne<Profile>()
            .WithMany()
            .HasForeignKey(x => x.ProfileId);
        builder.Property(x => x.DisplayOrder)
            .IsRequired()
            .HasMaxLength(100);
    }
}