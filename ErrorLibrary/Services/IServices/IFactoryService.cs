using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IFactoryService
    {
        Task<PagedList<FactoryDto>> GetAll(FactoryParam factoryParam);
        Task<List<Factory>> GetAll();
        Task<List<Factory>> GetAllByUnitId(int unitId);
        Task<Factory> GetById(int id);
        void Add(Factory factory);
        void Update(Factory factory);
        void Delete(Factory factory);
        Task<bool> CheckNameExists(string name, int unitId);
    }
}
