using LoyaltySystemApi.Domain.Entities.Users;
using LoyaltySystemApi.Domain.Services.Users;
using MediatR;

namespace LoyaltySystemApi.Application.UserManagement.Commands.UserSignUpCommand;

public record UserSignUpCommand(string Email, string Name, string Password, bool RegisterFromWeb, bool RegisterFromApp) : IRequest;

public sealed class UserSignUpCommandHandler : IRequestHandler<UserSignUpCommand>
{
    private readonly IUserService _userService;
    public UserSignUpCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task Handle(UserSignUpCommand request, CancellationToken cancellationToken)
    {
        var userEntity = new UserEntity(request.Email, request.Name, request.Password, request.RegisterFromWeb, request.RegisterFromApp);

        await _userService.SignUpAsync(userEntity, cancellationToken);
    }
}
