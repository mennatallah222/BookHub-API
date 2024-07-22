using API.Core.Bases;
using MediatR;

namespace API.Core.Features.Authentication.Commands.Models
{
    public class NewPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
