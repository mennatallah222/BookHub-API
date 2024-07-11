using API.Core.Features.Authentication.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("/Authentication/SignIn")]
        public async Task<IActionResult> CreateUser([FromForm] SignInCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
