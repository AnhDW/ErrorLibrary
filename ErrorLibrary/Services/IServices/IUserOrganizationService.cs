using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IUserOrganizationService
    {
        Task<PagedList<UserOrganizationDto>> GetAll(UserOrganizationParams userOrganizationParams);
        Task<List<UserOrganization>> GetAll();
        Task<List<string>> GetUserIdsByOrganizationId(string organizationType, int organizationId);
        Task<List<(string organizationType, int organizationId)>> GetOrganizationIdsByUserId(string userId);
        Task<UserOrganization> GetById(string userId, string organizationType, int organizationId);
        void Add(UserOrganization userOrganization);
        void Update(UserOrganization userOrganization);
        void Delete(UserOrganization userOrganization);
    }
}
