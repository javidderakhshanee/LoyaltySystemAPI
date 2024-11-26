using LoyaltySystemApi.Domain.Base;
using LoyaltySystemApi.Domain.Enums;

namespace LoyaltySystemApi.Domain.Entities.Points;

public sealed class PointEntity : BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required ActivityType ActivityType { get; set; }
    public required int Point { get; set; }
    public required int ExpirationNumDays { get; set; }
}
