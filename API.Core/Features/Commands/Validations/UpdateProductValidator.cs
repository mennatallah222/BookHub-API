using API.Core.Features.Commands.Models;
using API.Service.Interfaces;
using FluentValidation;

namespace API.Core.Features.Commands.Validations
{
    public class UpdateProductValidator : AbstractValidator<UpdateBookCommand>
    {
        #region ATTRIBUTES
        private readonly IProductsService _productsService;
        #endregion

        #region CTOR
        public UpdateProductValidator(IProductsService productsService)
        {
            _productsService = productsService;
            ApplyValidationRules();
        }
        #endregion

        #region ACTIONS

        public void ApplyValidationRules()
        {
            RuleFor(x => (x as UpdateBookCommand).Name).NotNull().WithMessage("Name must not be null!")
                                .NotEmpty().WithMessage("Name must not be empty!")
                                .MaximumLength(100).WithMessage("Name must be less than 100 characters!")
                                .MinimumLength(5).WithMessage("Name must be more than 5 characters!");

            RuleFor(x => (x as UpdateBookCommand).Price).NotNull().WithMessage("Price must not be null!")
                                 .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0")
                                 .NotEmpty().WithMessage("Price must not be empty!");

            RuleFor(x => (x as UpdateBookCommand).Quantity).NotNull().WithMessage("Quantity must not be null!")
                                 .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0");
        }


        #endregion

    }
}
