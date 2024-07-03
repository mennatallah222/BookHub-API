using API.Core.Bases;
using API.Core.Features.Commands.Models;
using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;

namespace API.Core.Features.Commands.Handlers
{
    public class OrderCommandHandler : Response_Handler,
        IRequestHandler<CreateOrderCommand, string>
    {
        private readonly IOrederService _orederService;
        private readonly IProductRepo _productRepo;
        private readonly ICustomer _customer;
        private readonly IMapper _mapper;

        public OrderCommandHandler(IOrederService orederService, IProductRepo productRepo, ICustomer customerRepo, IMapper mapper)
        {
            _orederService = orederService;
            _productRepo = productRepo;
            _customer = customerRepo;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);

            if (string.IsNullOrEmpty(order.PaymentMethod))
            {
                throw new ArgumentException("You must choose a Payment Method");

            }
            var pids = request.Products.Select(p => p.ProductId).ToList();
            var products = await _productRepo.GetProductsByIDS(pids);
            if (products == null || !products.Any()) return "Products not found for the given IDs";
            order.TotalAmount = 0;
            order.Products = new List<Product>();

            foreach (var op in request.Products)
            {
                var p = products.FirstOrDefault(x => x.ProductId == op.ProductId);
                p.Quantity -= op.Quantity;
                order.TotalAmount += p.Price * op.Quantity;
                order.Products.Add(p);
            }

            order.PaymentMethod = request.PaymentMethod;
            order.Status = "Pending";

            var res = await _orederService.AddOrderAsync(order);

            var productsToUpdate = order.Products.ToList();

            await _productRepo.UpadteRangeAsync(productsToUpdate);

            return "Succeeeded";
        }
    }
}
