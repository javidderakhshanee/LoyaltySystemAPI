using FluentValidation;
using LoyaltySystemApi.Domain.Errors;

namespace LoyaltySystemApi.Application.PointsManagement.Commands.UserEarnPointCommand;

public sealed class UserEarnPointCommandValidator : AbstractValidator<UserEarnPointCommand>
{
    public UserEarnPointCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty()
            .WithMessage(DomainErrors.User.InvalidUserId);
    }
}
