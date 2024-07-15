using API.Core.Bases;
using API.Core.Features.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Commands.Handlers
{
    public class BookCommandHandler : Response_Handler,
        IRequestHandler<CreateBookCommand, Response<string>>,
        IRequestHandler<UpdateBookCommand, Response<string>>,
        IRequestHandler<DeleteBookCommand, Response<string>>

    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public BookCommandHandler(IProductsService productsService,
                                     IMapper mapper,
                                  IStringLocalizer<SharedResources> localizer) : base(localizer)

        {
            _productsService = productsService;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            var res = await _productsService.AddProductAsync(product, request.GenresNames);
            //if (res == "") return BadRequest<string>("Name already exists!");
            /*var createdProduct = await _productsService.AddProductAsync(product);
            var response = _mapper.Map<CreateProductResponse>(createdProduct);
            */
            //if (res == "Success")
            return Created("Added!");
            // else
            //   return BadRequest<string>("An unknown error occurred");
        }

        public async Task<Response<string>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var product = await _productsService.GetProductByIdAsync(request.Id);
            if (product == null)
            {
                return NotFound<string>("Product is not found!");
            }
            _mapper.Map(request, product);
            await _productsService.UpdateProductAsync(product, request.BookGenres);
            return Success("product updated successfully!");

        }

        public async Task<Response<string>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            return Deleted<string>(await _productsService.DeleteProductAsync(request.BookId));
        }
    }
}
