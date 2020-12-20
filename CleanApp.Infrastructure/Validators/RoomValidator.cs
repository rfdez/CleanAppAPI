using CleanApp.Core.DTOs;
using FluentValidation;

namespace CleanApp.Infrastructure.Validators
{
    public class RoomValidator : AbstractValidator<RoomDto>
    {
        public RoomValidator()
        {
            RuleFor(room => room.HomeId)
                .NotNull()
                .NotEmpty();
            
            RuleFor(room => room.RoomName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(36);
        }
    }
}
