using API.Core.Features.Friends.Commands.Models;
using API.Core.Features.Friends.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FriendsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("SendFriendRequest")]
        public async Task<IActionResult> SendFriendRequest([FromForm] SendFriendRequestQuery command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("GetFriendRequest")]
        public async Task<IActionResult> GetFriendRequest([FromBody] GetFriendRequestQuery command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPost("AcceptFriendRequest")]
        public async Task<IActionResult> AcceptFriendRequest([FromBody] AcceptFriendRequestQuery command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("GetFriendsList")]
        public async Task<IActionResult> GetFriendsList([FromForm] GetFriendQuery command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }
}
