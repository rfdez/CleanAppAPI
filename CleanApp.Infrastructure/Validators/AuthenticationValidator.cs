using CleanApp.Core.DTOs;
using FluentValidation;

namespace CleanApp.Infrastructure.Validators
{
    public class AuthenticationValidator : AbstractValidator<AuthenticationDto>
    {
        public AuthenticationValidator()
        {
            RuleFor(auth => auth.UserLogin)
                .NotNull()
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(36);

            RuleFor(auth => auth.UserName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(36);

            RuleFor(auth => auth.UserPassword)
                .NotNull()
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(256)
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            RuleFor(auth => auth.UserRole)
                .NotNull()
                .NotEmpty()
                .IsInEnum();
        }
    }
}
