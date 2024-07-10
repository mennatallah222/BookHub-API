using API.Core.Features.UserFeatures.Queries.Response;
using API.Core.Wrappers;
using MediatR;

namespace API.Core.Features.UserFeatures.Queries.Models
{
    public class GetPaginatedUsersListQuery : IRequest<PaginatedResult<GetUsersListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
