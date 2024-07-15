
using API.Core.Bases;
using MediatR;

namespace API.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string roleName { get; set; }
    }
}
