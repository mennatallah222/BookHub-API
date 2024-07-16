
using API.Core.Bases;
using ClassLibrary1.Data_ClassLibrary1.Core.Responses;
using MediatR;

namespace API.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RfreshToken { get; set; }
    }
}
