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
        private readonly IFileService _fileService;

        public ApplicationUserService(
                                     UserManager<User> userManager,
                                     IHttpContextAccessor httpContextAccessor,
                                     IEmailService emailService,
                                     ApplicationDBContext dBContext,
                                     IUrlHelper urlHelper,
                                     IFileService fileService)
        {
            _userManager = userManager;
            _dbContext = dBContext;
            _contextAccessor = httpContextAccessor;
            _emailService = emailService;
            _urlHelper = urlHelper;
            _fileService = fileService;
        }


        public async Task<string> AddUserAsync(User user, string password, IFormFile file)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return "EmailExists";
                }

                var username = await _userManager.FindByNameAsync(user.UserName);
                if (username != null)
                {
                    return "UserNameExists";
                }

                var myContext = _contextAccessor.HttpContext.Request;
                var myBaseUrl = myContext.Scheme + "://" + myContext.Host;
                var imageUrl = await _fileService.UploadImage("Users", file);
                switch (imageUrl)
                {
                    case "NoImage": return "NoImage";
                    case "FailedToUploadTheImage": return "FailedToUploadTheImage";

                }
                user.Image = myBaseUrl + imageUrl;



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

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _contextAccessor.HttpContext.Request;
                var returnedUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                var message = $"To confirm email, click on: <a href='{returnedUrl}'> </a>";

                await _emailService.SendEmail(user.Email, returnedUrl, "Confrim Email");

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
