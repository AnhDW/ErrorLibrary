using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IEnterpriseService
    {
        Task<PagedList<EnterpriseDto>> GetAll(EnterpriseParam enterpriseParam);
        Task<List<Enterprise>> GetAll();
        Task<List<Enterprise>> GetAllByFactoryId(int factoryId);
        Task<Enterprise> GetById(int id);
        void Add(Enterprise enterprise);
        void Update(Enterprise enterprise);
        void Delete(Enterprise enterprise);
        Task<bool> CheckNameExists(string name, int factoryId);

    }
}
