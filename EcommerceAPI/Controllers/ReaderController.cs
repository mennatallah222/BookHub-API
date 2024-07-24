using API.Core.Features.Readers.Commands.Models;
using API.Core.Features.Readers.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public ReaderController(IMediator mediator)
        {
            _mediatR = mediator;
        }
        [HttpPost("AddToCurrentlyReading")]
        public async Task<IActionResult> AddToCurrentlyReading([FromBody] AddBookToCurrentlyReadingListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpPost("AddToReadList")]
        public async Task<IActionResult> AddToReadList([FromBody] AddBookToReadListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpPost("AddToWantToReadList")]
        public async Task<IActionResult> AddToWantToReadList([FromBody] AddBookToWantToReadListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpDelete("RemoveFromWantToReadList")]
        public async Task<IActionResult> RemoveFromWantToReadList([FromBody] RemoveBookFromWantToReadListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpDelete("RemoveFromReadList")]
        public async Task<IActionResult> RemoveFromReadList([FromBody] RemoveBookFromReadListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpDelete("RemoveFromCurrentlyReadingList")]
        public async Task<IActionResult> RemoveFromCurrentlyReadingList([FromBody] RemoveBookFromCurrentlyReadingListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpGet("{userId}/CurrentlyReadingList")]
        public async Task<IActionResult> GetCurrentlyReading(int userId)
        {
            var query = new GetCurrentlyReadingList(userId);
            var response = await _mediatR.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId}/WantToReadList")]
        public async Task<IActionResult> GetWantToRead(int userId)
        {
            var query = new GetWantToReadListQuery(userId);
            var response = await _mediatR.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId}/ReadList")]
        public async Task<IActionResult> GetReadList(int userId)
        {
            var query = new GetReadListQuery(userId);
            var response = await _mediatR.Send(query);
            return Ok(response);
        }


    }
}
