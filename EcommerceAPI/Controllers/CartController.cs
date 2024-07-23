using API.Core.Features.Commands.Models;
using API.Core.Features.Queries.Responses;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ICartService _cartService;
        public CartController(IMediator mediator, IMapper mapper, ICartService cartService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _cartService = cartService;

        }
        [HttpPost("AddProductToCart")]
        public async Task<IActionResult> AddProductToCart(AddProductToCartCommand command)
        {
            var res = await _mediator.Send(new AddProductToCartCommand
            {
                BookId = command.BookId,
                CustomerId = command.CustomerId,
                Quantity = command.Quantity
            });
            return Ok(res);
        }

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCustomerCart([FromHeader] int cid)
        {
            var src = await _cartService.GetCartContent(cid);
            var cartMapped = _mapper.Map<GetCartResponse>(src);
            return Ok(cartMapped);
        }
    }
}
