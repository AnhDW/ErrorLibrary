using Microsoft.AspNetCore.Identity;

namespace ErrorLibrary.Services.IServices
{
    public interface IUserRoleService
    {
        Task<List<IdentityUserRole<string>>> GetAll();
        Task<IdentityUserRole<string>> GetById(string userId, string roleId);
        Task<List<string>> GetRoleIdsByUserId(string userId);
        Task<List<string>> GetUserIdsByRoleId(string roleId);
        void Add(IdentityUserRole<string> identityUserRole);
        void Delete(IdentityUserRole<string> identityUserRole);
    }
}
