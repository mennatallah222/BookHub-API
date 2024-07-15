using API.Core.Bases;
using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using API.Core.SharedResource;
using API.Core.Wrappers;
using API.Service.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace API.Core.Features.Queries.Handlers
{
    public class BooksHandler : Response_Handler,
        IRequestHandler<GetAllBooksQuery, Response<List<GetAllBooksResponses>>>,
        IRequestHandler<GetBookByIdQuery, Response<GetAllBooksResponses>>,
        IRequestHandler<GetBookssPaginatedList, PaginatedResult<GetBookssPaginatedListResponse>>
    {
        private readonly IProductsService _productService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public BooksHandler(IProductsService productsService,
                               IMapper mapper,
                               IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _productService = productsService;
            _mapper = mapper;
            _localizer = stringLocalizer;
        }

        public async Task<Response<List<GetAllBooksResponses>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var src = await _productService.GetAllProductsAsync();
            if (src == null) return NotFound<List<GetAllBooksResponses>>(_localizer[SharedResourceKeys.AllNotFound]);

            var productsMapped = _mapper.Map<List<GetAllBooksResponses>>(src);
            return Success(productsMapped);
        }

        public async Task<Response<GetAllBooksResponses>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var src = await _productService.GetProductByIdAsync(request.Id);
            if (src == null) return NotFound<GetAllBooksResponses>(_localizer[SharedResourceKeys.NotFound]);
            var result = _mapper.Map<GetAllBooksResponses>(src);
            return Success(result);
        }
        public async Task<PaginatedResult<GetBookssPaginatedListResponse>> Handle(GetBookssPaginatedList request, CancellationToken cancellationToken)
        {
            Expression<Func<Product, GetBookssPaginatedListResponse>> expression = e => new GetBookssPaginatedListResponse(e.ProductId, e.Name, e.Price, e.Quantity, e.Description, e.BookGenres.FirstOrDefault().Genre.Name, e.Image);
            //var queryable = _productService.GetProductsQueryable();

            var filterQuery = _productService.FilterProductPaginationQueryable(request.OrderBy, request.Search);
            var paginatedList = await filterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedList;
        }


    }
}
