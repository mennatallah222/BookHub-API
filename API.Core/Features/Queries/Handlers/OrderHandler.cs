using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using API.Infrastructure.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
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
            var usersOrdersGrouped = orders.GroupBy(o => o.User.Id).Select(g => new GetOrdersHistoryResponse
            {
                UserId = g.Key,
                Name = g.First().User.FullName,
                Orders = g.Select(o => _mapper.Map<OrderDTOs>(o)).ToList()
            }).FirstOrDefault();

            if (usersOrdersGrouped == null) throw new Exception("No order history found!");
            return usersOrdersGrouped;
        }

    }
}
