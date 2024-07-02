using API.Core.Features.Commands.Models;
using API.Core.Features.Queries.Models;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
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
            var response=await _mediatR.Send(new GetAllProductsQuery());
            return Ok(response);
        }

        [HttpGet("/Product/{id}")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            var response = await _mediatR.Send(new GetProductByIdQuery { Id=id});
            return Ok(response);
        }

        [HttpPost("/Product/Add")]
        public async Task<IActionResult> PostProduct([FromBody] CreateProductCommand command)
        {
            var response=await _mediatR.Send(command);
           
            return Ok(response);
        }
    }
}
