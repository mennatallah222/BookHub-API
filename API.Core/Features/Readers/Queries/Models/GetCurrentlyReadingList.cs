using API.Core.Features.Readers.Queries.Responses;
using MediatR;

namespace API.Core.Features.Readers.Queries.Models
{
    public class GetCurrentlyReadingList : IRequest<GetCurrentlyReadingListResponse>
    {
        public int UserId { get; set; }
        public GetCurrentlyReadingList(int userId)
        {
            UserId = userId;
        }
    }
}
