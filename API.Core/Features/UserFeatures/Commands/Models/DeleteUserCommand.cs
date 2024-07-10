using API.Core.Bases;
using MediatR;

namespace API.Core.Features.UserFeatures.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
