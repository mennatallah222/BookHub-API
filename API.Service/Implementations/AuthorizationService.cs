using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Service.Implementations
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;

        public AuthorizationService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
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
    }
}
