using API.Core.Features.UserFeatures.Commands.Models;
using API.Core.SharedResource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.UserFeatures.Commands.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        #region attributes
        private readonly IStringLocalizer _localization;

        #endregion
        #region CTORS
        public ChangePasswordValidator(IStringLocalizer localization)
        {
            _localization = localization;
            ApplyValidationRules();
            ApplyCustomeValidationRules();
        }
        #endregion


        #region functions
        public void ApplyValidationRules()

        {                                             //add:  _localization[SharedResourcesKey.NotEmpty]

            RuleFor(x => x.CurrentPasswrod).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                           .NotNull().WithMessage(_localization[SharedResourceKeys.NotNull])
                                   ;

            RuleFor(z => z.NewPasswrod).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                       .NotNull().WithMessage(_localization[SharedResourceKeys.NotNull])
                                   ;

            RuleFor(x => x.ConfirmNewPasswrod).Equal(x => x.NewPasswrod).WithMessage(_localization[SharedResourceKeys.Equal])
                                 ;

            RuleFor(x => x.Id).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                              .NotNull().WithMessage(_localization[SharedResourceKeys.NotNull])
                                   ;


        }

        public void ApplyCustomeValidationRules()
        {

        }
        #endregion

    }
}
