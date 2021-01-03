using CleanApp.Core.Entities;
using FluentValidation;

namespace CleanApp.Infrastructure.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(userLogin => userLogin.User)
                .NotNull()
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(36);

            RuleFor(userLogin => userLogin.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(256)
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
        }
    }
}
