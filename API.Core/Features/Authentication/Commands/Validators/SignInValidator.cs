using API.Core.Features.Authentication.Commands.Models;
using API.Core.SharedResource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authentication.Commands.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        #region attributes
        private readonly IStringLocalizer _localization;

        #endregion
        #region CTORS
        public SignInValidator(IStringLocalizer localization)
        {
            _localization = localization;
            ApplyValidationRules();
            ApplyCustomeValidationRules();
        }
        #endregion


        #region functions
        public void ApplyValidationRules()

        {                                             //add:  _localization[SharedResourcesKey.NotEmpty]

            RuleFor(x => x.UserName).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.NotNull])
                              ;

            RuleFor(z => z.Password).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.NotNull]);



        }

        public void ApplyCustomeValidationRules()
        {

        }
        #endregion
    }
}
