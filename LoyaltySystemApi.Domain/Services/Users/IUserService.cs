using LoyaltySystemApi.Domain.Entities.Users;

namespace LoyaltySystemApi.Domain.Services.Users;

public interface IUserService
{
    Task AddEarnedPointAsync(UserPointEarnEntity earnEntity, CancellationToken cancellationToken);
    Task<bool> Exists(Guid userId, CancellationToken cancellationToken);
    Task<UserEntity> Get(Guid userId, CancellationToken cancellationToken);
    Task<int> GetPointsBalanceAsync(Guid userId, CancellationToken cancellationToken);
    Task SignUpAsync(UserEntity userEntity, CancellationToken cancellationToken);
    Task Update(UserEntity user, CancellationToken cancellationToken);
}
