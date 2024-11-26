using LoyaltySystemApi.Domain.Entities.Users;
using LoyaltySystemApi.Domain.Services.Users;
using LoyaltySystemApi.Infrastructure.Caches.Services;
using LoyaltySystemApi.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystemApi.Infrastructure.Services.Users;

public sealed class UserService : IUserService
{
    private readonly LoyaltySystemApiDbContext _dbContext;
    private readonly ICacheManagementService _cacheManagementService;
    private const string partCache = "EarnedUserPoints";
    public UserService(LoyaltySystemApiDbContext dbContext, ICacheManagementService cacheManagementService)
    {
        _dbContext = dbContext;
        _cacheManagementService = cacheManagementService;
    }

    public async Task AddEarnedPointAsync(UserPointEarnEntity earnedPoint, CancellationToken cancellationToken)
    {
        _dbContext.EarnedUserPoints.Add(earnedPoint);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var earnedPoints = await _cacheManagementService.Get<List<UserPointEarnEntity>>(earnedPoint.UserId.ToString(), partCache, cancellationToken);
        earnedPoints.Add(earnedPoint);

        await _cacheManagementService.Set(earnedPoint.UserId.ToString(), earnedPoints, partCache, cancellationToken: cancellationToken);
    }

    public async Task<bool> Exists(Guid userId, CancellationToken cancellationToken)
    {
        return (await _dbContext.Users.CountAsync(x => x.Id == userId, cancellationToken)) > 0;
    }

    public async Task<UserEntity> Get(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
    }

    public async Task<int> GetPointsBalanceAsync(Guid userId, CancellationToken cancellationToken)
    {
        var existsInCache = await _cacheManagementService.Exists<List<UserPointEarnEntity>>(userId.ToString(), partCache, cancellationToken);
        if (existsInCache)
            return (await _cacheManagementService.Get<List<UserPointEarnEntity>>(
                userId.ToString(),
                partCache,
                cancellationToken))
                .Sum(x => x.Point);

        var user = await _dbContext.Users
            .Where(x => x.Id == userId)
            .Include(x => x.EarnedPoints.Where(y => y.ExpireDate < DateTime.UtcNow))
            .FirstOrDefaultAsync(cancellationToken);

        var earnedPoints = user?.EarnedPoints;

        if (earnedPoints is null)
            return 0;

        await _cacheManagementService.Set(
            userId.ToString(),
            earnedPoints,
            partCache,
            cancellationToken: cancellationToken);

        return earnedPoints.Sum(x => x.Point);
    }

    public async Task SignUpAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        _dbContext.Users.Add(userEntity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(UserEntity user, CancellationToken cancellationToken)
    {
        await _dbContext.Users
               .Where(x => x.Id == user.Id)
               .ExecuteUpdateAsync(x =>
                            x.SetProperty(y => y.Name, user.Name)
                            .SetProperty(y => y.Mobile, user.Mobile)
                            .SetProperty(y => y.Birthday, user.Birthday)
                            .SetProperty(y => y.DateUpdated, user.DateUpdated)
                            , cancellationToken);
    }
}
