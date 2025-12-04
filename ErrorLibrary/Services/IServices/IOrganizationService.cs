namespace ErrorLibrary.Services.IServices
{
    public interface IOrganizationService
    {
        Task<IEnumerable<object>> GetOrganizationTree();
        Task<IEnumerable<object>> GetOrganizationTreeDropdown();
    }
}
