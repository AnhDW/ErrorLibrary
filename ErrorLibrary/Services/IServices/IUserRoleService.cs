using Microsoft.AspNetCore.Identity;

namespace ErrorLibrary.Services.IServices
{
    public interface IUserRoleService
    {
        Task<List<IdentityUserRole<string>>> GetAll();
        Task<List<string>> GetRoleIdsByUserId(string userId);
        Task<List<string>> GetUserIdsByRoleId(string roleId);
    }
}
