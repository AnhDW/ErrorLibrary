using ErrorLibrary.Entities;

namespace ErrorLibrary.Services
{
    public interface IRolePermissionService
    {
        Task<List<RolePermission>> GetAllRoles();
        Task<RolePermission> GetById(string role, int permissionId);
        Task<List<int>> GetPermissionIdsByRoleId(string roleId);
        Task<List<string>> GetRoleIdsByPermissionId(int permissionId);
        void Add(RolePermission rolePermission);
        void Delete(RolePermission rolePermission);
    }
}