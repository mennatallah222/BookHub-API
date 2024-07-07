using API.Core.Features.Queries.Responses;
using API.Core.Wrappers;
using ClassLibrary1.Data_ClassLibrary1.Core.Helpers;
using MediatR;

namespace API.Core.Features.Queries.Models
{
    public class GetProductsPaginatedList : IRequest<PaginatedResult<GetProductsPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ProductOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }

    }
}
