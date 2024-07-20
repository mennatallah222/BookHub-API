using API.Core.Features.Authentication.Commands.Models;
using API.Core.SharedResource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authentication.Commands.Validators
{
    public class ResestPasswordValidator : AbstractValidator<ResestPasswordCommand>
    {
        private readonly IStringLocalizer _localization;

        public ResestPasswordValidator(IStringLocalizer localization)
        {
            _localization = localization;
            ApplyValidationRules();
        }


        public void ApplyValidationRules()

        {                                             //add:  _localization[SharedResourcesKey.NotEmpty]

            RuleFor(x => x.Email).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.Required])
                              ;




        }

    }
}
