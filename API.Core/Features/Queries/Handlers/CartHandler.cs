using API.Core.Bases;
using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Core.Features.Queries.Handlers
{
    public class CartHandler : Response_Handler,
                                   IRequestHandler<GetCartQuery, Response<GetCartResponse>>
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        public CartHandler(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }
        public async Task<Response<GetCartResponse>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetCartContent(request.CustomerId);
            if (cart == null)
            {
                return NotFound<GetCartResponse>("Cart is not found");
            }
            var mappedResponse = _mapper.Map<GetCartResponse>(cart);
            return Success(mappedResponse);
        }
    }
}
