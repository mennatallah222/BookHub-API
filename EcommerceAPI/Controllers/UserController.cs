using API.Core.Features.UserFeatures.Commands.Models;
using API.Core.Features.UserFeatures.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //  [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("/User/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetUserByIDQuery(id));
            return Ok(response);
        }
        [Authorize(Policy = "GetUsers")]
        [HttpGet("/User/PaginatedList")]
        public async Task<IActionResult> GetPaginatedUserList([FromQuery] GetPaginatedUsersListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("/User/AddUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] AddUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
        [HttpPut("/User/UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
        [HttpDelete("/User/DeleteUser")]
        [Authorize(Roles = "Admin, Author")]

        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("/User/ChangeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

    }
}
