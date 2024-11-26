using LoyaltySystemApi.Domain.Entities.Points;
using LoyaltySystemApi.Domain.Enums;
using LoyaltySystemApi.Domain.Services.EarnPoints;
using LoyaltySystemApi.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystemApi.Infrastructure.Services.Points;

public sealed class PointService : IPointService
{
    private readonly LoyaltySystemApiDbContext _dbContext;
    public PointService(LoyaltySystemApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<PointEntity> GetByActivityType(ActivityType activityType, CancellationToken cancellationToken)
    {
        return await _dbContext.Points
            .FirstOrDefaultAsync(x => x.ActivityType == activityType, cancellationToken);
    }
}
