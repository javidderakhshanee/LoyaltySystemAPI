using LoyaltySystemApi.Domain.Base;

namespace LoyaltySystemApi.Domain.Entities.Rewards;

public sealed class RewardCatalogEntity : BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int MinPointsToEarn { get; set; }
    public int MaxPointsToEarn { get; set; }
    public required string Title { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
