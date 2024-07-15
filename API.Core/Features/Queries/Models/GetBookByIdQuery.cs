using API.Core.Bases;
using API.Core.Features.Queries.Responses;
using MediatR;

namespace API.Core.Features.Queries.Models
{
    public class GetBookByIdQuery : IRequest<Response<GetAllBooksResponses>>
    {
        public int Id { get; set; }
    }
}
