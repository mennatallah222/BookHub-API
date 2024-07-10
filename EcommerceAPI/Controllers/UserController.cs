using API.Core.Features.UserFeatures.Commands.Models;
using API.Core.Features.UserFeatures.Queries.Models;
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

        [HttpGet("/User/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetUserByIDQuery(id));
            return Ok(response);
        }
        [HttpGet("/User/PaginatedList")]
        public async Task<IActionResult> GetPaginatedUserList([FromQuery] GetPaginatedUsersListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("/User/AddUser")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
