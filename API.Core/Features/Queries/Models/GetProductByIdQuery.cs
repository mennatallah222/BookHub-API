using API.Core.Bases;
using API.Core.Features.Queries.Responses;
using MediatR;

namespace API.Core.Features.Queries.Models
{
    public class GetProductByIdQuery : IRequest<Response<GetAllProductsResponses>>
    {
        public int Id { get; set; }
    }
}
