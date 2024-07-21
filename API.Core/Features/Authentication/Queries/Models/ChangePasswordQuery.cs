using API.Core.Bases;
using MediatR;

namespace API.Core.Features.Authentication.Queries.Models
{
    public class ChangePasswordQuery : IRequest<Response<string>>
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
