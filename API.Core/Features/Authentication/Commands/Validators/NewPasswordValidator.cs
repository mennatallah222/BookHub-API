using API.Core.Features.Authentication.Commands.Models;
using API.Core.SharedResource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authentication.Commands.Validators
{
    public class NewPasswordValidator : AbstractValidator<NewPasswordCommand>
    {
        private readonly IStringLocalizer _localization;

        public NewPasswordValidator(IStringLocalizer localization)
        {
            _localization = localization;
            ApplyValidationRules();
        }


        public void ApplyValidationRules()

        {                                             //add:  _localization[SharedResourcesKey.NotEmpty]

            RuleFor(x => x.Email).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.Required])
                              ;


            RuleFor(x => x.Password).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.Required]);


            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.Required]);


        }
    }
}