using API.Core.Features.Authentication.Commands.Models;
using API.Core.Features.Authentication.Queries.Models;
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
        [HttpPost("SignIn")]
        public async Task<IActionResult> CreateUser([FromForm] SignInCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("Validate-Token")]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpPost("SendForResetPassword")]
        public async Task<IActionResult> SendForResetPassword([FromQuery] ResestPasswordCommand query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("ConfirmResetPassword")]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ChangePasswordQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] NewPasswordCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

    }
}
