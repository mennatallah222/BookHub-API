﻿using API.Core.Bases;
using API.Core.Features.Commands.Models;
using API.Core.SharedResource;
using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Commands.Handlers
{
    public class OrderCommandHandler : Response_Handler,
                                      IRequestHandler<CreateOrderCommand, string>
    {
        private readonly IOrederService _orederService;
        private readonly IProductRepo _productRepo;
        private readonly ICartRepo _cartRepo;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;


        public OrderCommandHandler(IOrederService orederService,
                                   IProductRepo productRepo,
                                   ICartRepo cartRepo,
                                   IMapper mapper,
                                  IStringLocalizer<SharedResources> localizer) : base(localizer)

        {
            _orederService = orederService;
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);

            if (string.IsNullOrEmpty(order.PaymentMethod))
            {
                throw new ArgumentException("You must choose a Payment Method");

            }
            var cart = await _cartRepo.GetCartByIDAsync(request.UserId);
            var pids = cart.CartItems.Select(p => p.ProductId).ToList();
            var products = await _productRepo.GetProductsByIDS(pids);
            if (products == null || !products.Any()) return "Product's not found for the given IDs";
            order.TotalAmount = 0;
            order.OrderItems = new List<OrderItem>();

            foreach (var cartItem in cart.CartItems)
            {
                var p = products.FirstOrDefault(x => x.ProductId == cartItem.ProductId);
                if (p == null)
                {
                    return $"Product with ID {cartItem.ProductId} is not found!";
                }

                var orderItem = new OrderItem
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    Price = p.Price
                };
                p.Quantity -= cartItem.Quantity;
                order.TotalAmount += p.Price * cartItem.Quantity;
                order.OrderItems.Add(orderItem);
            }

            order.PaymentMethod = request.PaymentMethod;
            order.Status = "Pending";

            var res = await _orederService.AddOrderAsync(order);

            var productsToUpdate = order.OrderItems.ToList();

            await _productRepo.UpadteRangeAsync(products);
            await _cartRepo.ClearCartAsync(request.UserId);
            return "Succeeeded";
        }
    }
}
