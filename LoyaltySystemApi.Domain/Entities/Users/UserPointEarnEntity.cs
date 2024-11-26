namespace LoyaltySystemApi.Domain.Entities.Users;

public sealed class UserPointEarnEntity
{
    public UserPointEarnEntity(Guid userId, Guid pointId, DateTime expiredDate, int point)
    {
        UserId = userId;
        PointId = pointId;
        ExpireDate = expiredDate;
        Point = point;
    }

    public Guid UserId { get; private set; }
    public Guid PointId { get; private set; }
    public DateTime ExpireDate { get; private set; }
    public int Point { get; private set; }
    public UserEntity User { get; private set; }
}
