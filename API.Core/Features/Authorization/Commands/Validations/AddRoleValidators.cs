using API.Core.Features.Authorization.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authorization.Commands.Validations
{
    public class AddRoleValidators : AbstractValidator<AddRoleCommand>
    {

        #region attributes
        private readonly IStringLocalizer<SharedResources> _localization;
        private readonly IAuthorizationService _authorizationService;

        #endregion
        #region CTORS
        public AddRoleValidators(IStringLocalizer<SharedResources> localization,
                                 IAuthorizationService authorizationService)
        {
            _localization = localization;
            _authorizationService = authorizationService;

            ApplyValidationRules();
            ApplyCustomeValidationRules();
        }
        #endregion


        #region functions
        public void ApplyValidationRules()

        {                                             //add:  _localization[SharedResourcesKey.NotEmpty]

            RuleFor(x => x.RoleName).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.NotNull])
                              ;


        }

        public void ApplyCustomeValidationRules()
        {
            RuleFor(x => x.RoleName)                                   //if it is true, print the message XXXXXXXXX
                                                                       //so we reversed it, if it is false, print the message
                        .MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsNameExists(Key))
                        .WithMessage("Product name already exists!");
        }
        #endregion
    }
}
