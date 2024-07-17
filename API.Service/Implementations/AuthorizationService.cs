using API.Infrastructure.Data;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using ClassLibrary1.Data_ClassLibrary1.Core.Helpers;
using ClassLibrary1.Data_ClassLibrary1.Core.Requests;
using ClassLibrary1.Data_ClassLibrary1.Core.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Service.Implementations
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _dBContext;

        public AuthorizationService(RoleManager<Role> roleManager,
            UserManager<User> userManager,
            ApplicationDBContext dBContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dBContext = dBContext;
        }
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "Successeeded";
            return "Failed";

        }



        public async Task<bool> IsNameExists(string name)
        {
            /* var role = await _roleManager.FindByNameAsync(name);
             if (role == null) return false;

             return true;*/
            return await _roleManager.RoleExistsAsync(name);
        }

        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            //check if it exists or not
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null) return "NotFound";
            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Success";

            var errors = string.Join(", ", result.Errors);
            return errors;
        }

        public async Task<List<Role>> GetRolesList()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<Role> GetRolesById(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<ManageUserRoleResponse> ManageUserRolesData(User user)
        {
            var response = new ManageUserRoleResponse();
            var newUserRoles = new List<UserRoles>();
            //user roles
            //all roles
            var roles = await _roleManager.Roles.ToListAsync();

            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userRole = new UserRoles();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRole.HasRole = true;
                }
                else
                {
                    userRole.HasRole = false;
                }
                newUserRoles.Add(userRole);
            }
            response.UserRoles = newUserRoles;
            return response;

        }

        public async Task<string> AddRoleToUserAsync(int userId, string roleName)
        {
            var response = new ManageUserRoleResponse();

            var user = await _userManager.FindByIdAsync(userId.ToString());
            var role = await _roleManager.FindByNameAsync(roleName);
            //user roles
            var userRoles = await _userManager.GetRolesAsync(user);
            //all roles

            if (userRoles.Contains(roleName))
            {
                return "The role you entered is alrady assigned to the user";
            }
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return "Role added to the user successfully";
            }

            return "Failed to add role to the user";
        }

        public async Task<ManageUserClaimResponse> ManageUserClaimssData(User user)
        {
            var response = new ManageUserClaimResponse();
            var userClaimsList = new List<UserClaims>();
            response.UserId = user.Id;
            //get user
            //get user claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in ClaimStore.claims)
            {
                var userClaim = new UserClaims();
                userClaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                {
                    userClaim.Value = true;
                }
                else
                {
                    userClaim.Value = false;
                }
                userClaimsList.Add(userClaim);
            }
            //if claim exists for user then value is true
            response.UserClaims = userClaimsList;
            return response;
            //return result
        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {
            var transact = await _dBContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                var userClaims = await _userManager.GetClaimsAsync(user);
                var removeClaims = await _userManager.RemoveClaimsAsync(user, userClaims);
                var claims = request.UserClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
                if (!removeClaims.Succeeded) return "FailedToRemoveClaims";


                var addedClaims = await _userManager.AddClaimsAsync(user, claims);
                if (!addedClaims.Succeeded) return "FailedToAddNewClaims";

                await transact.CommitAsync();

                return "Success";

            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateClaims";
            }
        }
    }
}
