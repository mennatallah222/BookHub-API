﻿namespace API.Service.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsNameExists(string name);
    }
}
