using API.Core.Bases;
using MediatR;

namespace API.Core.Features.Readers.Commands.Models
{
    public class RemoveBookFromWantToReadListCommand : IRequest<Response<string>>
    {
        public int UserID { get; set; }
        public int BookID { get; set; }
    }
}
