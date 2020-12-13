using CleanApp.Core.DTOs;
using FluentValidation;

namespace CleanApp.Infrastructure.Validators
{
    public class HomeValidator : AbstractValidator<HomeDto>
    {
        public HomeValidator()
        {
            RuleFor(home => home.HomeAddress)
                .NotNull()
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(256);

            RuleFor(home => home.HomeCity)
                .NotNull()
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(36);

            RuleFor(home => home.HomeCountry)
                .NotNull()
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(36);

            RuleFor(home => home.HomeZipCode)
                .NotNull()
                .NotEmpty()
                .Matches(@"^(?:0[1-9]|[1-4]\d|5[0-2])\d{3}$");
        }
    }
}
