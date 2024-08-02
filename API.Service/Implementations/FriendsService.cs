using API.Infrastructure.Data;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Service.Implementations
{
    public class FriendsService : IFriendsService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<User> _userManager;

        public FriendsService(ApplicationDBContext dbContext,
                              UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task SendFriendRequestNotification(Notification notification)
        {
            _dbContext.Notifications.Add(notification);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> SendFriendRequestAsync(int fromUserId, int toUserId)
        {
            if (fromUserId == toUserId)
            {
                return "You can't send a friend request to yourself!";
            }

            var existingRequest = await _dbContext.Friendships
                .FirstOrDefaultAsync(f => (f.UserId == fromUserId && f.FriendId == toUserId) ||
                                          (f.UserId == toUserId && f.FriendId == fromUserId));
            if (existingRequest != null)
            {
                if (existingRequest.FriendshipStatus == "Pending")
                {
                    return "You already sent a friend request!";
                }
                else if (existingRequest.FriendshipStatus == "Accepted")
                {
                    return "You are already friends with this user";
                }
            }

            var friendRequest = new Friendship
            {
                UserId = fromUserId,
                FriendId = toUserId,
                FriendshipStatus = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            await _dbContext.Friendships.AddAsync(friendRequest);

            var notificationResult = await SendFriendRequestNotificationAsync(friendRequest);
            if (notificationResult != "Success")
            {
                return notificationResult;
            }

            return "Friend request sent successfully!";
        }

        public async Task<string> SendFriendRequestNotificationAsync(Friendship friendRequest)
        {
            var user = await _userManager.FindByIdAsync(friendRequest.FriendId.ToString());
            if (user == null)
            {
                return "User not found";
            }

            var notification = new Notification
            {
                UserId = friendRequest.UserId,
                Message = $"You have a new friend request from user {friendRequest.Friend.FullName}",
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _dbContext.Notifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }



        public async Task<string> AcceptFriendRequestAsync(int userAcceptingId, int friendId)
        {
            var request = await _dbContext.Friendships
                .FirstOrDefaultAsync(f => f.UserId == friendId && f.FriendId == userAcceptingId && f.FriendshipStatus == "Pending");
            if (request == null)
            {
                return "Friend request not found or already accepted";
            }
            request.FriendshipStatus = "Accepted";
            _dbContext.Friendships.Update(request);
            await _dbContext.SaveChangesAsync();

            await SendFriendRequestNotificationAsync(new Friendship
            {
                UserId = userAcceptingId,
                FriendId = friendId,
                FriendshipStatus = "Accepted",
                CreatedAt = DateTime.UtcNow
            });

            var userToNotify = await _userManager.FindByIdAsync(friendId.ToString());
            if (userToNotify != null)
            {
                var notification = new Notification
                {
                    UserId = friendId,
                    Message = $"Your friend request to user with ID {userAcceptingId} has been accepted",
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                };

                await _dbContext.Notifications.AddAsync(notification);
                await _dbContext.SaveChangesAsync();
            }

            return "Friend request accepted successfully!";
        }

        public async Task<List<Friendship>> GetPendingFriendRequestsAsync(int userId)
        {
            return await _dbContext.Friendships
                .Include(f => f.User)
                .Include(f => f.Friend)
                .Where(f => f.FriendId == userId && f.FriendshipStatus == "Pending")
                .ToListAsync();
        }

        public async Task<List<Friendship>> GetAcceptedFriendRequestsAsync(int userId)
        {
            return await _dbContext.Friendships
                .Include(f => f.User)
                .Include(f => f.Friend)
                .Where(f => (f.UserId == userId || f.FriendId == userId) && f.FriendshipStatus == "Accepted")
                .ToListAsync();
        }
    }
}
