using LoyaltySystemApi.Application.CustomExceptions.Users;
using LoyaltySystemApi.Domain.Services.Users;
using MediatR;

namespace LoyaltySystemApi.Application.UserManagement.Commands.UserUpdateProfileCommand;

public record UserUpdateProfileCommand(Guid Id, string Name, string Mobile, DateTime? Birthday) : IRequest;

public sealed class UserUpdateProfileCommandHandler : IRequestHandler<UserUpdateProfileCommand>
{
    private readonly IUserService _userService;
    public UserUpdateProfileCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task Handle(UserUpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.Get(request.Id, cancellationToken);
        if (user is null)
            throw new UserNotFoundException();

        user = user.UpdateData(request.Name, request.Mobile, request.Birthday);

        await _userService.Update(user, cancellationToken);
    }
}
