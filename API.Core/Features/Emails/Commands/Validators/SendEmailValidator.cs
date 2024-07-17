using API.Core.Features.Emails.Commands.Models;
using API.Core.SharedResource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Emails.Commands.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        private readonly IStringLocalizer _localization;

        public SendEmailValidator(IStringLocalizer localization)
        {
            _localization = localization;
            ApplyValidationRules();
        }


        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                   .NotNull().WithMessage(_localization[SharedResourceKeys.NotNull])
                             ;

            RuleFor(z => z.Message).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.NotNull]);


        }
    }
}
