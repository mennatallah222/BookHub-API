
using API.Core.Bases;
using ClassLibrary1.Data_ClassLibrary1.Core.Responses;
using MediatR;

namespace API.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
