using API.Core.Bases;
using API.Core.Features.Commands.Models;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;

namespace API.Core.Features.Commands.Handlers
{
    public class CartCommandHandler : Response_Handler, IRequestHandler<AddProductToCartCommand, string>
    {
        private readonly IProductsService _productService;
        private readonly ICustomerService _customerService;
        private readonly ICartService _cartService;
        public CartCommandHandler(ICustomerService customerService, ICartService cartService, IProductsService productsService)
        {
            _productService = productsService;
            _customerService = customerService;
            _cartService = cartService;
        }

        public async Task<string> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetByIdAsync(request.CustomerId);
            var product = await _productService.GetProductByIdAsync(request.ProductId);
            if (product == null) return "Product is not found!";
            var cart = await _cartService.GetCartContent(customer.CustomerId);
            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = request.CustomerId,
                    CreatedAt = DateTime.UtcNow,
                };
                await _cartService.AddCart(cart);
            }

            var cartItems = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);
            if (cartItems != null) cartItems.Quantity += request.Quantity;
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    CartId = cart.Id
                });
            }
            _cartService.UpdateCart(cart);
            return "Products added successfully";
        }


    }
}
