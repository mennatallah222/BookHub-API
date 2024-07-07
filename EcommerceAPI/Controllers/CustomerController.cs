using API.Core.Features.Queries.Models;
using MediatR;
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

        [HttpGet("/Customer/GetList")]
        public async Task<IActionResult> GetCustomerList()
        {
            var response = await _mediator.Send(new GetCustomerListQuery());
            return Ok(response);
        }

        [HttpGet("/Customer/GetCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var response = await _mediator.Send(new GetCustomerByID { Id = id });
            return Ok(response);
        }
    }
}
