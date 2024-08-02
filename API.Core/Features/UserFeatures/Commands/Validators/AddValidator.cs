using API.Core.Features.UserFeatures.Commands.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.User.Commands.Validators
{
    public class AddValidator : AbstractValidator<AddUserCommand>
    {
        #region attributes
        private readonly IStringLocalizer _localization;

        #endregion
        #region CTORS
        public AddValidator(IStringLocalizer localization)
        {
            _localization = localization;
            ApplyValidationRules();
        }
        #endregion


        #region functions
        public void ApplyValidationRules()

        {
            RuleFor(x => x.FullName).NotNull().WithMessage("Full Name must not be null!")
                                .NotEmpty().WithMessage("Full Name must not be empty!")
                              .MaximumLength(100).WithMessage("Name must be less than 100 characters!")
                              .MinimumLength(5).WithMessage("Name must be more than 5 characters!");
            RuleFor(z => z.Email).NotNull().WithMessage("Email must not be null!")
                                 .NotEmpty().WithMessage("Email must not be empty!");
            RuleFor(x => x.UserName).NotNull().WithMessage("UserName must not be null!")
                                 ;

            RuleFor(x => x.Password).NotNull().WithMessage("UserName must not be null!")
                                    .NotEmpty().WithMessage("Full Name must not be empty!");

            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("You must Confirm your Password and not be null!")
                                    .Equal(x => x.Password)
                                    .NotEmpty().WithMessage("Your password confirmation does not match!");


        }


        #endregion
    }
}
