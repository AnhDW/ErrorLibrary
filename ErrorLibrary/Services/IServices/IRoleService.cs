using ErrorLibrary.Entities;

namespace ErrorLibrary.Services.IServices
{
    public interface IRoleService
    {
        Task<List<ApplicationRole>> GetAll();
        Task<List<ApplicationRole>> GetRoleByRoleIds(List<string> roleIds);
        Task<ApplicationRole> GetById(string id);
        void Add(ApplicationRole role);
        void Update(ApplicationRole role);
        void Delete(ApplicationRole role);

        Task<bool> CheckNameExists(string name);
    }
}
