using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using ClassLibrary1.Data_ClassLibrary1.Core.Requests;
using ClassLibrary1.Data_ClassLibrary1.Core.Responses;

namespace API.Service.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsNameExists(string name);
        public Task<string> EditRoleAsync(EditRoleRequest editRoleRequest);
        public Task<List<Role>> GetRolesList();
        public Task<Role> GetRolesById(int id);
        public Task<ManageUserRoleResponse> ManageUserRolesData(User user);
        public Task<string> AddRoleToUserAsync(int uId, string roleName);

        public Task<ManageUserClaimResponse> ManageUserClaimssData(User user);

    }
}
