using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Service.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsNameExists(string name);
        public Task<string> EditRoleAsync(EditRoleRequest editRoleRequest);
        public Task<List<Role>> GetRolesList();
        public Task<Role> GetRolesById(int id);
    }
}
