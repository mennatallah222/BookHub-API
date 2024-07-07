using API.Core.Bases;
using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using API.Core.Wrappers;
using API.Service.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;
using System.Linq.Expressions;

namespace API.Core.Features.Queries.Handlers
{
    public class ProductsHandler : Response_Handler, IRequestHandler<GetAllProductsQuery, Response<List<GetAllProductsResponses>>>,
                                   IRequestHandler<GetProductByIdQuery, Response<GetAllProductsResponses>>,
                                   IRequestHandler<GetProductsPaginatedList, PaginatedResult<GetProductsPaginatedListResponse>>


    {
        private readonly IProductsService _productService;
        private readonly IMapper _mapper;
        public ProductsHandler(IProductsService productsService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productsService;
        }
        public async Task<Response<List<GetAllProductsResponses>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var src = await _productService.GetAllProductsAsync();
            var productsMapped = _mapper.Map<List<GetAllProductsResponses>>(src);
            return Success(productsMapped);
        }
        public async Task<Response<GetAllProductsResponses>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var src = await _productService.GetProductByIdAsync(request.Id);
            if (src == null) return NotFound<GetAllProductsResponses>("Product is not found!");
            var result = _mapper.Map<GetAllProductsResponses>(src);
            return Success(result);
        }


        public async Task<PaginatedResult<GetProductsPaginatedListResponse>> Handle(GetProductsPaginatedList request, CancellationToken cancellationToken)
        {
            Expression<Func<Product, GetProductsPaginatedListResponse>> expression = e => new GetProductsPaginatedListResponse(e.ProductId, e.Name, e.Price, e.Quantity, e.Description, e.Category.Name, e.Image);
            //var queryable = _productService.GetProductsQueryable();

            var filterQuery = _productService.FilterProductPaginationQueryable(request.Search);
            var paginatedList = await filterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedList;
        }


    }
}
