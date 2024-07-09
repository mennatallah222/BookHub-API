using API.Core.Features.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Commands.Validations
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        #region ATTRIBUTES
        private readonly IProductsService _productsService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion

        #region CTOR
        public CreateProductValidator(IProductsService productsService,
                                      IStringLocalizer<SharedResources> localizer)
        {
            _productsService = productsService;
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomeValidationRules();
        }
        #endregion

        #region ACTIONS

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name).NotNull().WithMessage(_localizer[SharedResourceKeys.NotNull])
                                .NotEmpty().WithMessage(_localizer[SharedResourceKeys.NotEmpty])
                              .MaximumLength(100).WithMessage("Name must be less than 100 characters!")
                              .MinimumLength(5).WithMessage("Name must be more than 5 characters!");
            RuleFor(z => z.Price).NotNull().WithMessage("Price must not be null!")
                                 .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0")
                                 .NotEmpty().WithMessage("Price must not be empty!");
            RuleFor(x => x.Quantity).NotNull().WithMessage("Quantity must not be null!")
                                 .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0")
                                 ;
        }

        public void ApplyCustomeValidationRules()
        {
            RuleFor(x => x.Name)                                   //if it is true, print the message XXXXXXXXX
                                                                   //so we reversed it, if it is false, print the message
                        .MustAsync(async (Key, CancellationToken) => !await _productsService.IsNameExist(Key))
                        .WithMessage("Product name already exists!");
        }
        #endregion

    }
}
