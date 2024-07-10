using API.Core.Bases;
using MediatR;

namespace API.Core.Features.UserFeatures.Commands.Models
{
    public class ChangeUserPasswordCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string CurrentPasswrod { get; set; }
        public string NewPasswrod { get; set; }
        public string ConfirmNewPasswrod { get; set; }
    }
}
