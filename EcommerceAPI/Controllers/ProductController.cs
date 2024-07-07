using API.Core.Features.Commands.Models;
using API.Core.Features.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public ProductController(IMediator mediator)
        {
            _mediatR = mediator;
        }
        [HttpGet("/Product/List")]
        public async Task<IActionResult> GetProductList()
        {
            var response = await _mediatR.Send(new GetAllProductsQuery());
            return Ok(response);
        }

        [HttpGet("/Product/{id}")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            var response = await _mediatR.Send(new GetProductByIdQuery { Id = id });
            return Ok(response);
        }

        [HttpPost("/Product/Add")]
        public async Task<IActionResult> PostProduct([FromBody] CreateProductCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpPut("/Product/Update")]
        public async Task<IActionResult> PutProduct([FromBody] UpdateProductCommand command)
        {
            var response = await _mediatR.Send(command);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }


        [HttpGet("/Product/PaginatedList")]
        public async Task<IActionResult> GetPaginatedProductList([FromQuery] GetProductsPaginatedList query)
        {
            var response = await _mediatR.Send(query);
            return Ok(response);
        }


    }
}
