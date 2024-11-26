using LoyaltySystemApi.Application.CustomExceptions.Points;
using LoyaltySystemApi.Application.CustomExceptions.Users;
using LoyaltySystemApi.Domain.Entities.Users;
using LoyaltySystemApi.Domain.Enums;
using LoyaltySystemApi.Domain.Services.EarnPoints;
using LoyaltySystemApi.Domain.Services.Users;
using MediatR;

namespace LoyaltySystemApi.Application.PointsManagement.Commands.UserEarnPointCommand;

public record UserEarnPointCommand(Guid UserId, ActivityType ActivityType) : IRequest;

public sealed class UserEarnPointCommandHandler : IRequestHandler<UserEarnPointCommand>
{
    private readonly IUserService _userService;
    private readonly IPointService _pointService;
    public UserEarnPointCommandHandler(IUserService userService, IPointService earnPointService)
    {
        _userService = userService;
        _pointService = earnPointService;
    }
    public async Task Handle(UserEarnPointCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.Get(request.UserId, cancellationToken);
        if (user is null)
            throw new UserNotFoundException();

        var pointInf = await _pointService.GetByActivityType(request.ActivityType, cancellationToken);
        if (pointInf is null)
            throw new PointNotFoundException();

        var expiredDate = DateTime.UtcNow.AddDays(pointInf.ExpirationNumDays);
        var earnEntity = new UserPointEarnEntity(request.UserId, pointInf.Id, expiredDate, pointInf.Point);

        await _userService.AddEarnedPointAsync(earnEntity, cancellationToken);
    }
}
