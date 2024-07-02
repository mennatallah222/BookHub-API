using API.Core.Features.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/Customer/List")]
        public async Task<IActionResult> GetCustomerList()
        {
            var response = await _mediator.Send(new GetCustomerListQuery());
            return Ok(response);
        }

        [HttpGet("/Customer/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var response = await _mediator.Send(new GetCustomerByID { Id=id});
            return Ok(response);
        }
    }
}
