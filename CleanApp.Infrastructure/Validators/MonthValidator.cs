using CleanApp.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Infrastructure.Validators
{
    public class MonthValidator : AbstractValidator<MonthDto>
    {
        public MonthValidator()
        {
            RuleFor(month => month.YearId)
                .NotNull()
                .NotEmpty();

            RuleFor(month => month.MonthValue)
                .NotNull()
                .NotEmpty()
                .LessThan(12)
                .GreaterThan(0);
        }
    }
}
