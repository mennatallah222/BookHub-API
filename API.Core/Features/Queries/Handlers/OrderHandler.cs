using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using API.Infrastructure.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Core.Features.Queries.Handlers
{
    public class OrderHandler : IRequestHandler<GetOrderQuery, GetOrdersHistoryResponse>
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        public OrderHandler(IOrderRepo orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }
        public async Task<GetOrdersHistoryResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepo.GetListAsync();
            if (orders == null)
            {
                throw new Exception("No order history found!");
            }
            var mappedResponse = _mapper.Map<GetOrdersHistoryResponse>(orders);
            return mappedResponse;
        }

    }
}
