using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using MediatR;

namespace API.Core.Features.Friends.Queries.Models
{
    public class GetFriendQuery : IRequest<List<FriendshipDto>>
    {
        public int UserId { get; set; }
        public GetFriendQuery()
        {

        }
        public GetFriendQuery(int userId)
        {
            UserId = userId;
        }
    }
}
