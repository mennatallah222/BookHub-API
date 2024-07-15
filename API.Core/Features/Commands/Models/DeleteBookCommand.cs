using API.Core.Bases;
using MediatR;

namespace API.Core.Features.Commands.Models
{
    public class DeleteBookCommand : IRequest<Response<string>>
    {
        public int BookId { get; set; }
    }
}
