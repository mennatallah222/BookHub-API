using API.Core.Features.Commands.Models;
using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using API.Infrastructure.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IProductRepo _productRepo;
        private readonly ICustomer _customerRepo;

        public OrderController(IMediator mediator, IMapper mapper, IProductRepo productRepo, ICustomer customerRepo)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productRepo = productRepo;
            _customerRepo = customerRepo;
        }

        [HttpPost("/Order/Add/CustomerId")]
        public async Task<IActionResult> PostOrder([FromBody] CreateOrderCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerOrders([FromHeader] int cid)
        {
            var query = new GetOrderQuery { CustomerId = cid };
            var response = await _mediator.Send(query);

            var ordersHistoryDto = _mapper.Map<GetOrdersHistoryResponse>(response);
            return Ok(ordersHistoryDto);
        }
    }
}
/*
 var order = _mapper.Map<Order>(command);
            var pids = command.Products.Select(p => p.ProductId).ToList();
            var products = await _productRepo.GetProductsByIDS(pids);
            if (products == null || !products.Any()) return NotFound("Products not found for the given IDs");

            order.TotalAmount = 0;
            foreach (var op in command.Products)
            {
                var p = products.FirstOrDefault(x => x.ProductId == op.ProductId);
                if (p != null)
                {
                    p.Quantity -= op.Quantity;
                    order.TotalAmount += p.Price * op.Quantity;
                    order.Products.Add(p);
                }
                else
                {
                    return BadRequest("Product with ID {op.ProductId} not found in the order");
                }
            }
 */