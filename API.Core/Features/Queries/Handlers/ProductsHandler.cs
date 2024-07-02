using API.Core.Bases;
using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using API.Service.Implementations;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Core.Features.Queries.Handlers
{
    public class ProductsHandler : Response_Handler, IRequestHandler<GetAllProductsQuery, Response<List<GetAllProductsResponses>>>,
                                   IRequestHandler<GetProductByIdQuery, Response<GetAllProductsResponses>>

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
            var productsMapped=_mapper.Map<List<GetAllProductsResponses>>(src);
            return Success(productsMapped);
        }
        public async Task<Response<GetAllProductsResponses>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var src = await _productService.GetProductByIdAsync(request.Id);
            if (src == null) return NotFound<GetAllProductsResponses>("Product is not found!");
            var result=_mapper.Map<GetAllProductsResponses>(src);
            return Success(result);
        }
    }
}
