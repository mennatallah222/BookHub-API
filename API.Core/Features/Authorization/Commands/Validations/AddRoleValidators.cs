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

        {

            RuleFor(x => x.RoleName).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.NotNull])
                              ;


        }

        public void ApplyCustomeValidationRules()
        {
            RuleFor(x => x.RoleName)
                        .MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsNameExists(Key))
                        .WithMessage("Product name already exists!");
        }
        #endregion
    }
}
