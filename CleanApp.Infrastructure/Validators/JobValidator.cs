using CleanApp.Core.DTOs;
using FluentValidation;

namespace CleanApp.Infrastructure.Validators
{
    public class JobValidator : AbstractValidator<JobDto>
    {
        public JobValidator()
        {
            RuleFor(job => job.RoomId)
                .NotNull()
                .NotEmpty();

            RuleFor(job => job.JobName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(36);

            RuleFor(job => job.JobDescription)
                .NotNull()
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(256);
        }
    }
}
