using API.Core.Bases;
using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Queries.Handlers
{
    public class CartHandler : Response_Handler,
                                   IRequestHandler<GetCartQuery, Response<GetCartResponse>>
    {
        private readonly ICartService _cartService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;
        public CartHandler(ICartService cartService,
                           IMapper mapper,
                                  IStringLocalizer<SharedResources> localizer) : base(localizer)

        {
            _cartService = cartService;
            _mapper = mapper;
            _localizer = localizer;
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
