using API.Core.Bases;
using MediatR;

namespace API.Core.Features.Friends.Commands.Models
{
    public class SendFriendRequestQuery : IRequest<Response<string>>
    {
        public int FriendId { get; set; }
        public int UserSenderId { get; set; }
    }
}
