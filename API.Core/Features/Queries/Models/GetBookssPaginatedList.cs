﻿using API.Core.Features.Queries.Responses;
using API.Core.Wrappers;
using ClassLibrary1.Data_ClassLibrary1.Core.Enums;
using MediatR;

namespace API.Core.Features.Queries.Models
{
    public class GetBookssPaginatedList : IRequest<PaginatedResult<GetBookssPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ProductOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }

    }
}
