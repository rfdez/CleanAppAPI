using CleanApp.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Infrastructure.Validators
{
    public class TenantValidator : AbstractValidator<TenantDto>
    {
        public TenantValidator()
        {
            RuleFor(tenant => tenant.TenantName)
                .NotNull()
                .NotEmpty();
        }
    }
}
