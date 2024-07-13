using API.Core.Features.Authorization.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorizationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("/Authorization/AddRole")]
        public async Task<IActionResult> AddRole([FromForm] AddRoleCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
