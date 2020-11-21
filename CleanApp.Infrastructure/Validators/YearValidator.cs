using CleanApp.Core.DTOs;
using FluentValidation;

namespace CleanApp.Infrastructure.Validators
{
    public class YearValidator : AbstractValidator<YearDto>
    {
        public YearValidator()
        {
            RuleFor(year => year.YearValue)
                .NotNull()
                .LessThan(2100)
                .GreaterThan(2000);
        }
    }
}
