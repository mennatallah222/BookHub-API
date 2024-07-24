using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Service.Interfaces
{
    public interface IFriendsService
    {
        public Task<string> SendFriendRequestAsync(int fromUserId, int toUserId);
        public Task<string> SendFriendRequestNotificationAsync(Friendship friendRequest);
        public Task<string> AcceptFriendRequestAsync(int userAcceptingId, int friendId);
        Task<List<Friendship>> GetPendingFriendRequestsAsync(int userId);
        public Task<List<Friendship>> GetAcceptedFriendRequestsAsync(int userId);

    }
}
