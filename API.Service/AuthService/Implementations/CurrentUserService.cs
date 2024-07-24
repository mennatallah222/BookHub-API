using API.Service.AuthService.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using ClassLibrary1.Data_ClassLibrary1.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace API.Service.AuthService.Implementations
{
    public class CurrentUserService : ICurrentUserService
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;

        public CurrentUserService(IHttpContextAccessor contextAccessor,
                                UserManager<User> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        public int GetUserId()
        {
            var userId = _contextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == nameof(UserClaimModel.Id)).Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            return int.Parse(userId);
        }

        public async Task<User> GetUserAsync()
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            return user;
        }

        public async Task<List<string>> GetUserRoleAsync()
        {
            var user = await GetUserAsync();
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}
