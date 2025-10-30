namespace ErrorLibrary.Services.IServices
{
    public interface IOrganizationService
    {
        Task<IEnumerable<object>> GetOrganizationTree();
    }
}
