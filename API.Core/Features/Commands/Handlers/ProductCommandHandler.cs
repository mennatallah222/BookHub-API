using API.Core.Bases;
using API.Core.Features.Commands.Models;
using API.Service.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;

namespace API.Core.Features.Commands.Handlers
{
    public class ProductCommandHandler : Response_Handler,
        IRequestHandler<CreateProductCommand, Response<string>>
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public ProductCommandHandler(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            var res = await _productsService.AddProductAsync(product);
            //if (res == "") return BadRequest<string>("Name already exists!");
            /*var createdProduct = await _productsService.AddProductAsync(product);
            var response = _mapper.Map<CreateProductResponse>(createdProduct);
            */
            //if (res == "Success")
            return Created("Added!");
            // else
            //   return BadRequest<string>("An unknown error occurred");
        }
    }
}
