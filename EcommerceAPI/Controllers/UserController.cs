using API.Core.Features.UserFeatures.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost("/User/AddUser")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
