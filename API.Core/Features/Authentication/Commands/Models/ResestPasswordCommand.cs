using API.Core.Bases;
using MediatR;

namespace API.Core.Features.Authentication.Commands.Models
{
    public class ResestPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
