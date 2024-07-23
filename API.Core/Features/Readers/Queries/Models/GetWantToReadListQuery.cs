using API.Core.Features.Readers.Queries.Responses;
using MediatR;

namespace API.Core.Features.Readers.Queries.Models
{
    public class GetWantToReadListQuery : IRequest<GetWantToReadListResponse>
    {
        public int UserId { get; set; }
        public GetWantToReadListQuery(int userId)
        {
            UserId = userId;
        }
    }
}
