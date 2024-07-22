using API.Core.Features.Readers.Commands.Models;
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
        [HttpPost("/Reader/AddToCurrentlyReading")]
        // [Authorize(Roles = "Admin, Author")]
        public async Task<IActionResult> AddToCurrentlyReading([FromBody] AddBookToCurrentlyReadingListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpPost("/Reader/AddToReadList")]
        // [Authorize(Roles = "Admin, Author")]
        public async Task<IActionResult> AddToReadList([FromBody] AddBookToReadListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpPost("/Reader/AddToWantToReadList")]
        public async Task<IActionResult> AddToWantToReadList([FromBody] AddBookToWantToReadListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpDelete("/Reader/RemoveFromWantToReadList")]
        public async Task<IActionResult> RemoveFromWantToReadList([FromBody] RemoveBookFromWantToReadListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpDelete("/Reader/RemoveFromReadList")]
        public async Task<IActionResult> RemoveFromReadList([FromBody] RemoveBookFromReadListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpDelete("/Reader/RemoveFromCurrentlyReadingList")]
        public async Task<IActionResult> RemoveFromCurrentlyReadingList([FromBody] RemoveBookFromCurrentlyReadingListCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

    }
}
