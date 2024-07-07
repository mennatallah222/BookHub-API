using API.Core.Features.Queries.Responses;
using API.Core.Wrappers;
using MediatR;

namespace API.Core.Features.Queries.Models
{
    public class GetProductsPaginatedList : IRequest<PaginatedResult<GetProductsPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string[]? OrderBy { get; set; }
        public string? Search { get; set; }

    }
}
