using LoyaltySystemApi.Domain.Entities.Users;
using LoyaltySystemApi.Infrastructure.EF.Configs.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltySystemApi.Infrastructure.EF.Configs.Users;

public sealed class UserConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
             .IsRequired()
             .HasMaxLength(50);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Mobile)
            .HasMaxLength(12);

        builder.Property(x => x.Password)
            .HasMaxLength(500)
            .HasConversion<PasswordEncryptingConversion>();

        builder.HasMany(x => x.Activities)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.ActivityId);

        builder.HasMany(x => x.EarnedPoints)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.PointId);
    }
}
