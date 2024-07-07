using API.Core.Features.Queries.Responses;
using MediatR;

namespace API.Core.Features.Queries.Models
{
    public class GetOrderQuery : IRequest<GetOrdersHistoryResponse>
    {
        public int CustomerId { get; set; }
    }
}
