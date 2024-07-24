using MediatR;

namespace API.Core.Features.Friends.Commands.Models
{
    public class AcceptFriendRequestQuery : IRequest<string>
    {
        public int UserAccepting { get; set; }
        public int FriendId { get; set; }
        public AcceptFriendRequestQuery(int userAccepting, int friendId)
        {
            UserAccepting = userAccepting;
            FriendId = friendId;
        }
    }
}
