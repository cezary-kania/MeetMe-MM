using MeetMe.Modules.Matching.Core.Entities;
using MeetMe.Modules.Matching.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMe.Modules.Matching.Core.DAL.Configurations;

internal class UserFilterConfiguration : IEntityTypeConfiguration<UserFilter>
{
    public void Configure(EntityTypeBuilder<UserFilter> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.MinAge)
            .IsRequired()
            .HasMaxLength(5);
        builder.Property(x => x.MaxAge)
            .IsRequired()
            .HasMaxLength(5);
        builder.Property(x => x.Gender)
            .IsRequired()
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<Gender>(x)
            );
    }
}