using API.Core.Features.Authorization.Commands.Models;
using API.Core.Features.Authorization.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
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

        [HttpPut("/Authorization/UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromForm] EditRoleCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("/Authorization/GetRoleList")]
        public async Task<IActionResult> GetRoleList()
        {
            var response = await _mediator.Send(new GetRoleListQuery());

            return Ok(response);
        }

        [HttpGet("/Authorization/GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetRoleByIdQuery() { Id = id });

            return Ok(response);
        }

        [HttpGet("/Authorization/ManageUserRoles/{id}")]
        public async Task<IActionResult> ManageUserRoles([FromRoute] int id)
        {
            var response = await _mediator.Send(new ManageUserRoleQuery() { UserId = id });

            return Ok(response);
        }

        [HttpPost("/Authorization/AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser([FromBody] UpdateUserRolesCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("/Authorization/ManageUserClaims/{id}")]
        public async Task<IActionResult> ManageUserClaims([FromRoute] int id)
        {
            var response = await _mediator.Send(new ManageUserClaimQuery() { UserId = id });

            return Ok(response);
        }

        [HttpPost("/Authorization/UpdateUserClaims")]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
