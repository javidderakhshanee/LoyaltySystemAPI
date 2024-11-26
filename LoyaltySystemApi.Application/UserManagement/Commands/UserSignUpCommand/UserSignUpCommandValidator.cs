using FluentValidation;
using LoyaltySystemApi.Domain.Errors;

namespace LoyaltySystemApi.Application.UserManagement.Commands.UserSignUpCommand;

public sealed class UserSignUpCommandValidator:AbstractValidator<UserSignUpCommand>
{
    public UserSignUpCommandValidator()
    {
        RuleFor(v => v.Email)
       .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
       .WithMessage(DomainErrors.User.InvalidEmailAddress);

        RuleFor(v => v.Name)
       .NotEmpty()
       .WithMessage(DomainErrors.User.NameRequired);

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage(DomainErrors.User.PasswordRequired)
            .MinimumLength(8).WithMessage(DomainErrors.User.PasswordMustBe)
            .MaximumLength(16).WithMessage(DomainErrors.User.PasswordMustBe)
            .Matches(@"[A-Z]+").WithMessage(DomainErrors.User.PasswordMustBe)
            .Matches(@"[a-z]+").WithMessage(DomainErrors.User.PasswordMustBe)
            .Matches(@"[0-9]+").WithMessage(DomainErrors.User.PasswordMustBe)
            .Matches(@"[\!\?\*\.]+").WithMessage(DomainErrors.User.PasswordMustBe);
    }
}
