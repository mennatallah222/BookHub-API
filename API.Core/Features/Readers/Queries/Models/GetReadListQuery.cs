using API.Core.Features.Readers.Queries.Responses;
using MediatR;

namespace API.Core.Features.Readers.Queries.Models
{
    public class GetReadListQuery : IRequest<GetReadListResponse>
    {
        public int UserId { get; set; }
        public GetReadListQuery(int userId)
        {
            UserId = userId;
        }
    }
}
