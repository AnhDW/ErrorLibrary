using ErrorLibrary.DTOs;

namespace ErrorLibrary.Services.IServices
{
    public interface IOrganizationService
    {
        Task<IEnumerable<object>> GetOrganizationTree();
        Task<IEnumerable<object>> GetOrganizationTreeDropdown();
        Task<List<OrganizationDisplayDto>> GetAllOrganizationsDisplay();
        Task<List<OrganizationDisplayDto>> GetFactoriesOrganizationsDisplay();
    }
}
