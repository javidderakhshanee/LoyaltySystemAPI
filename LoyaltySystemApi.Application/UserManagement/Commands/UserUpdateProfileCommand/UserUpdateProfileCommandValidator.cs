using FluentValidation;
using LoyaltySystemApi.Domain.Errors;
using System.Text.RegularExpressions;

namespace LoyaltySystemApi.Application.UserManagement.Commands.UserUpdateProfileCommand;

public sealed class UserUpdateProfileCommandValidator:AbstractValidator<UserUpdateProfileCommand>
{
    public UserUpdateProfileCommandValidator()
    {
        RuleFor(v => v.Id)
       .NotEmpty()
       .WithMessage(DomainErrors.User.InvalidUserId);

        RuleFor(v => v.Name)
       .NotEmpty()
       .WithMessage(DomainErrors.User.NameRequired);

        RuleFor(p => p.Mobile)
            .MinimumLength(11).WithMessage(DomainErrors.User.MobileMeBe)
            .MaximumLength(11).WithMessage(DomainErrors.User.MobileMeBe)
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage(DomainErrors.User.MobileMeBe)
            .When(s => !string.IsNullOrEmpty(s.Mobile)); 
    }
}
