using CleanApp.Core.DTOs;
using FluentValidation;

namespace CleanApp.Infrastructure.Validators
{
    public class CleanlinessValidator : AbstractValidator<CleanlinessDto>
    {
        public CleanlinessValidator()
        {
            RuleFor(cleanliness => cleanliness.RoomId)
                .NotNull()
                .NotEmpty();

            RuleFor(cleanliness => cleanliness.WeekId)
                .NotNull()
                .NotEmpty();

            RuleFor(cleanliness => cleanliness.TenantId)
                .NotEmpty()
                .NotNull();

            RuleFor(cleanliness => cleanliness.CleanlinessDone)
                .NotNull();
        }
    }
}
