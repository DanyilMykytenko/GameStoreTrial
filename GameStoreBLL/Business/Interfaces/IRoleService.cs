using Business.Models;

namespace Business.Interfaces
{
    public interface IRoleService
    {
        Task<RoleModel> GetRoleById(string id);
        Task<IEnumerable<RoleModel>> GetAllRoles();
        Task CreateRole(CreateRoleModel model);
        Task UpdateRole(UpdateRoleModel model);
        Task DeleteRole(string id);
        Task<IEnumerable<string>> GetRolesFromUser(string userId);
        Task<IEnumerable<string>> GetPermissions();
        Task<IEnumerable<string>> GetPermissionsFromRole(string userId);
    }
}
