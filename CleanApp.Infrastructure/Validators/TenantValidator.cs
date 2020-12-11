using CleanApp.Core.DTOs;
using FluentValidation;

namespace CleanApp.Infrastructure.Validators
{
    public class TenantValidator : AbstractValidator<TenantDto>
    {
        public TenantValidator()
        {
            RuleFor(tenant => tenant.AuthUser)
                .NotNull()
                .NotEmpty();

            RuleFor(tenant => tenant.TenantName)
                .NotNull()
                .NotEmpty();
        }
    }
}
