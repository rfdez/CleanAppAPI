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
                .NotEmpty();

            RuleFor(home => home.HomeCity)
                .NotNull()
                .NotEmpty();

            RuleFor(home => home.HomeCountry)
                .NotNull()
                .NotEmpty();

            RuleFor(home => home.HomeZipCode)
                .NotNull()
                .NotEmpty()
                .Matches(@"^(?:0[1-9]|[1-4]\d|5[0-2])\d{3}$");
        }
    }
}
