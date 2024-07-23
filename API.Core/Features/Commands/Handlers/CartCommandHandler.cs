using API.Core.Bases;
using API.Core.Features.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Commands.Handlers
{
    public class CartCommandHandler : Response_Handler, IRequestHandler<AddProductToCartCommand, string>
    {
        private readonly IProductsService _productService;
        private readonly UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> _userManager;
        private readonly ICartService _cartService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public CartCommandHandler(UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> userManager,
                                  ICartService cartService,
                                  IProductsService productsService,
                                  IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _productService = productsService;
            _userManager = userManager;
            _cartService = cartService;
            _localizer = localizer;
        }

        public async Task<string> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.CustomerId.ToString());
            var product = await _productService.GetProductByIdAsync(request.BookId);
            if (product == null) return "Product is not found!";
            var cart = await _cartService.GetCartContent(user.Id);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = request.CustomerId,
                    CreatedAt = DateTime.UtcNow,
                };
                await _cartService.AddCart(cart);
            }

            var cartItems = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.BookId);
            if (cartItems != null) cartItems.Quantity += request.Quantity;
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = request.BookId,
                    Quantity = request.Quantity,
                    CartId = cart.Id
                });
            }
            _cartService.UpdateCart(cart);
            return "Products added successfully";
        }


    }
}
