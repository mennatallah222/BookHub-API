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
    }
}
