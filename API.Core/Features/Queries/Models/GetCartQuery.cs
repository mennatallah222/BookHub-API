using API.Core.Bases;
using API.Core.Features.Queries.Responses;
using MediatR;

namespace API.Core.Features.Queries.Models
{
    public class GetCartQuery : IRequest<Response<GetCartResponse>>
    {
        public int UserId { get; set; }
    }
}
