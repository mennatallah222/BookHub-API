using API.Infrastructure.Data;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Service.Implementations
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailService _emailService;
        private readonly ApplicationDBContext _dbContext;
        private readonly IUrlHelper _urlHelper;

        public ApplicationUserService(
                                     UserManager<User> userManager,
                                     IHttpContextAccessor httpContextAccessor,
                                     IEmailService emailService,
                                     ApplicationDBContext dBContext,
                                     IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _dbContext = dBContext;
            _contextAccessor = httpContextAccessor;
            _emailService = emailService;
            _urlHelper = urlHelper;
        }


        public async Task<string> AddUserAsync(User user, string password)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // If email already exists
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return "EmailExists";
                }

                // If username already exists
                var username = await _userManager.FindByNameAsync(user.UserName);
                if (username != null)
                {
                    return "UserNameExists";
                }

                // Map request to User entity
                // var identityUser = _mapper.Map<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User>(request);

                // Create user
                var createdUser = await _userManager.CreateAsync(user, password);
                if (!createdUser.Succeeded)
                {
                    return string.Join(", ", createdUser.Errors.Select(x => x.Description).ToList());
                }

                var usersList = await _userManager.Users.ToListAsync();
                if (usersList.Count >= 0)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }

                //send confirming email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _contextAccessor.HttpContext.Request;
                var returnedUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                // $"/Authentication/ConfirmEmail?userId={user.Id}&code={code}";

                //message body
                var message = await _emailService.SendEmail(user.Email, returnedUrl);

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "Failed";

            }
        }
    }
}
