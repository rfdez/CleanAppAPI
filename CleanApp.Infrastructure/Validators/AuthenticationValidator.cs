using CleanApp.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Infrastructure.Validators
{
    public class AuthenticationValidator : AbstractValidator<AuthenticationDto>
    {
        public AuthenticationValidator()
        {
            RuleFor(auth => auth.CurrentUser)
                .NotNull()
                .NotEmpty();
            
            RuleFor(auth => auth.UserName)
                .NotNull()
                .NotEmpty();
            
            RuleFor(auth => auth.UserPassword)
                .NotNull()
                .NotEmpty();
        }
    }
}
