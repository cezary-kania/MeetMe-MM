using MeetMe.Modules.Users.Domain.Entities;
using MeetMe.Modules.Users.Domain.valueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMe.Modules.Users.Infrastructure.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new UserId(x));
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, x => new Email(x))
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.PasswordHash)
            .HasConversion(x => x.Value, x => new PasswordHash(x))
            .IsRequired()
            .HasMaxLength(200);
    }
}