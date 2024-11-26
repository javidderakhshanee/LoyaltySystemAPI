using LoyaltySystemApi.Domain.Entities.Points;
using LoyaltySystemApi.Domain.Enums;

namespace LoyaltySystemApi.Domain.Services.EarnPoints;

public interface IPointService
{
    Task<PointEntity> GetByActivityType(ActivityType activityType, CancellationToken cancellationToken);
}
