

using CleanApp.Core.DTOs;
using FluentValidation;

namespace CleanApp.Infrastructure.Validators
{
    public class WeekValidator : AbstractValidator<WeekDto>
    {
        public WeekValidator()
        {
            RuleFor(week => week.MonthId)
                .NotNull()
                .NotEmpty();

            RuleFor(week => week.WeekValue)
                .NotNull()
                .NotEmpty()
                .LessThan(5)
                .GreaterThan(0);
        }
    }
}
