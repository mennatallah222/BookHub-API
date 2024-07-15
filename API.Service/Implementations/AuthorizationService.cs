﻿using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Service.Implementations
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public AuthorizationService(RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

        public async Task<ManageUserRoleResponse> GetManageUserRolesData(User user)
        {
            var response = new ManageUserRoleResponse();
            var newUserRoles = new List<UserRoles>();
            //user roles
            var userRoles = await _userManager.GetRolesAsync(user);
            //all roles
            var roles = await _roleManager.Roles.ToListAsync();

            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userRole = new UserRoles();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                if (userRoles.Contains(role.Name))
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
    }
}
