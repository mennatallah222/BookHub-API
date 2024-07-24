using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using MediatR;

namespace API.Core.Features.Friends.Queries.Models
{
    public class GetFriendRequestQuery : IRequest<List<FriendshipDto>>
    {
        public int UserId { get; set; }
        public GetFriendRequestQuery(int userId)
        {
            UserId = userId;
        }
    }
}
