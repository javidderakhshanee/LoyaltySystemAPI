using LoyaltySystemApi.Application.CustomExceptions.Users;
using LoyaltySystemApi.Domain.Models.Users;
using LoyaltySystemApi.Domain.Services.Users;
using MediatR;

namespace LoyaltySystemApi.Application.PointsManagement.Queries;

public record UserBalancePointsQuery(Guid Id) : IRequest<UserBalancePointModel>;

public sealed class UserBalancePointsQueryHandler : IRequestHandler<UserBalancePointsQuery, UserBalancePointModel>
{
    private readonly IUserService _userService;
    public UserBalancePointsQueryHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<UserBalancePointModel> Handle(UserBalancePointsQuery request, CancellationToken cancellationToken)
    {
        var userExists = await _userService.Exists(request.Id, cancellationToken);
        if (!userExists)
            throw new UserNotFoundException();

        var pointsBalance = await _userService.GetPointsBalanceAsync(request.Id, cancellationToken);

        return new UserBalancePointModel(pointsBalance);
    }
}