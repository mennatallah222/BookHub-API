using API.Core.Features.Authentication.Queries.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authentication.Queries.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        #region attributes
        private readonly IStringLocalizer<SharedResources> _localization;
        private readonly IAuthorizationService _authorizationService;

        #endregion
        #region CTORS
        public ConfirmEmailValidator(IStringLocalizer<SharedResources> localization,
                                 IAuthorizationService authorizationService)
        {
            _localization = localization;
            _authorizationService = authorizationService;

            ApplyValidationRules();
        }
        #endregion


        public void ApplyValidationRules()

        {                                             //add:  _localization[SharedResourcesKey.NotEmpty]

            RuleFor(x => x.UserId).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.Required])
                              ;

            RuleFor(x => x.Code).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.Required])
                              ;

        }

    }

}