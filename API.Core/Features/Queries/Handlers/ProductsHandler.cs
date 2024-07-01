using API.Core.Bases;
using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using API.Service.Implementations;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using MediatR.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Features.Queries.Handlers
{
    public class ProductsHandler : Response_Handler, IRequestHandler<GetAllProductsQuery, Response<List<GetAllProductsResponses>>>
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
        /*public async Task<Response<GetSingleProductResponses>> Handle()
        {

        }*/
    }
}
