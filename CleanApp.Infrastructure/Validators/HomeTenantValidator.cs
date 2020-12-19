using CleanApp.Core.DTOs;
using FluentValidation;

namespace CleanApp.Infrastructure.Validators
{
    public class HomeTenantValidator : AbstractValidator<HomeTenantDto>
    {
        public HomeTenantValidator()
        {
            RuleFor(homeTenant => homeTenant.HomeId)
                .NotNull()
                .NotEmpty();

            RuleFor(homeTenant => homeTenant.TenantId)
                .NotNull()
                .NotEmpty();

            RuleFor(homeTenant => homeTenant.TenantRole)
                .IsInEnum();
        }
    }
}
