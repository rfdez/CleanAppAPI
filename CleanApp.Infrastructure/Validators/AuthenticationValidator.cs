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
                .NotEmpty();

            RuleFor(auth => auth.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(auth => auth.UserPassword)
                .NotNull()
                .NotEmpty()
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            RuleFor(auth => auth.UserRole)
                .IsInEnum();
        }
    }
}
