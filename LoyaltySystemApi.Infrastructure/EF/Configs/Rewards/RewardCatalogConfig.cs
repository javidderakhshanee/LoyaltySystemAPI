using LoyaltySystemApi.Domain.Entities.Rewards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltySystemApi.Infrastructure.EF.Configs.Rewards;

public sealed class RewardCatalogConfig : IEntityTypeConfiguration<RewardCatalogEntity>
{
    public void Configure(EntityTypeBuilder<RewardCatalogEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
             .IsRequired()
             .HasMaxLength(50);
    }
}
