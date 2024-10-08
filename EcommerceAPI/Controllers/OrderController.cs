﻿using API.Core.Features.Commands.Models;
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

        public OrderController(IMediator mediator, IMapper mapper, IProductRepo productRepo)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productRepo = productRepo;
        }

        [HttpPost("MakeOrder/UserId")]
        public async Task<IActionResult> PostOrder([FromBody] CreateOrderCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("GetOrdersHistory/userId")]
        public async Task<IActionResult> GetCustomerOrders([FromHeader] int userId)
        {
            var query = new GetOrderQuery { UserId = userId };
            var response = await _mediator.Send(query);

            var ordersHistoryDto = _mapper.Map<GetOrdersHistoryResponse>(response);
            return Ok(ordersHistoryDto);
        }
    }
}
