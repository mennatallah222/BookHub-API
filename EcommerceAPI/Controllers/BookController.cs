using API.Core.Features.Commands.Models;
using API.Core.Features.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public BookController(IMediator mediator)
        {
            _mediatR = mediator;
        }

        [AllowAnonymous]
        [HttpGet("/Book/List")]
        public async Task<IActionResult> GetBookList()
        {
            var response = await _mediatR.Send(new GetAllBooksQuery());
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("/Book/{id}")]
        public async Task<IActionResult> GetBookByID(int id)
        {
            var response = await _mediatR.Send(new GetBookByIdQuery { Id = id });
            return Ok(response);
        }

        [HttpPost("/Book/Add")]
        // [Authorize(Roles = "Admin, Author")]

        public async Task<IActionResult> PostBook([FromBody] CreateBookCommand command)
        {
            var response = await _mediatR.Send(command);

            return Ok(response);
        }

        [HttpPut("/Book/Update")]
        // [Authorize(Roles = "Admin, Author")]

        public async Task<IActionResult> PutBook(int id, [FromBody] UpdateBookCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the command.");
            }
            var response = await _mediatR.Send(command);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [AllowAnonymous]
        [HttpGet("/Book/PaginatedList")]
        public async Task<IActionResult> GetPaginatedBookList([FromQuery] GetBookssPaginatedList query)
        {
            var response = await _mediatR.Send(query);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpDelete("/Book/DeleteBook{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var response = await _mediatR.Send(new DeleteBookCommand { BookId = id });
            return Ok(response);
        }


    }
}
