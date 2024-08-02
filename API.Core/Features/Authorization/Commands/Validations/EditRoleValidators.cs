using API.Core.Features.Authorization.Commands.Models;
using API.Core.SharedResource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authorization.Commands.Validations
{
    public class EditRoleValidators : AbstractValidator<EditRoleCommand>
    {

        #region attributes
        private readonly IStringLocalizer<SharedResources> _localization;

        #endregion
        #region CTORS
        public EditRoleValidators(IStringLocalizer<SharedResources> localization)
        {
            _localization = localization;

            ApplyValidationRules();
        }
        #endregion


        #region functions
        public void ApplyValidationRules()

        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.NotNull])
                              ;
            RuleFor(x => x.Name).NotEmpty().WithMessage(_localization[SharedResourceKeys.NotEmpty])
                                    .NotNull().WithMessage(_localization[SharedResourceKeys.NotNull])
                              ;

        }


        #endregion
    }
}
