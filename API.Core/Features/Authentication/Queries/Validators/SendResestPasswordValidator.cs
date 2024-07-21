using API.Core.Features.Authentication.Queries.Models;
using API.Core.SharedResource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authentication.Queries.Validators
{
    public class SendResestPasswordValidator : AbstractValidator<ChangePasswordQuery>
    {
        private readonly IStringLocalizer _localization;

        public SendResestPasswordValidator(IStringLocalizer localization)
        {
            _localization = localization;
            ApplyValidationRules();
        }


        public void ApplyValidationRules()

        {                                             //add:  _localization[SharedResourcesKey.NotEmpty]

            RuleFor(x => x.Code).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.Required])
                              ;

            RuleFor(x => x.Email).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.Required])
                              ;
        }

    }
}
