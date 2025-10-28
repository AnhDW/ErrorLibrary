using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper;
using ErrorLibrary.Helper.EntityParams;

namespace ErrorLibrary.Services.IServices
{
    public interface IUnitService
    {
        Task<PagedList<UnitDto>> GetAll(UnitParam unitParam);
        Task<List<Unit>> GetAll();
        Task<Unit> GetById(int id);
        void Add(Unit unit);
        void Update(Unit unit);
        void Delete(Unit unit);
    }
}
