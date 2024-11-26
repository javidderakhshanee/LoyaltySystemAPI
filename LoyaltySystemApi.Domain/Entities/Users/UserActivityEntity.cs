using LoyaltySystemApi.Domain.Base;
using LoyaltySystemApi.Domain.Enums;

namespace LoyaltySystemApi.Domain.Entities.Users;

public sealed class UserActivityEntity : BaseEntity
{
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public required Guid ActivityId { get; set; }
    public required ActivityType ActivityType{ get; set; }
    public Guid UserId { get; set; }
    public required UserEntity User { get; set; }
}
